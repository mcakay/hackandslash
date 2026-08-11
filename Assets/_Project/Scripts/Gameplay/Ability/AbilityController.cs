using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class AbilityController : MonoBehaviour
{
	[SerializeField] private AbilityConfigSO config;
	[SerializeField] private MovesetSO initialMoveset;

	public AbilityBuffer Buffer { get; private set; }
	public AbilityTracker Tracker { get; private set; }
	public Moveset Moveset { get; private set; }
	public StateMachine StateMachine { get; private set; }
	public LocalEventChannel Channel { get; private set; }

	public bool CanEarlyCancel { get; set; } = false;

	private void Awake()
	{
		Channel = GetComponent<LocalEventChannel>();

		StateMachine = new StateMachine();

		Moveset = new Moveset(this, initialMoveset);
		Buffer = new AbilityBuffer(config, this);
		Tracker = new AbilityTracker(this);

		StateMachine.AddState(new WindupState(this));
		StateMachine.AddState(new ExecutionState(this));
		StateMachine.AddState(new RecoveryState(this));
	}

	private void OnEnable()
	{
		Channel.Subscribe<WeaponEquippedEvent>(OnWeaponEquip);
	}

	private void OnDisable()
	{
		Channel.Unsubscribe<WeaponEquippedEvent>(OnWeaponEquip);
	}

	private void Update()
	{
		bool isExecuting = StateMachine.CurrentState != null;

		Buffer.Process();
		Tracker.Tick(isExecuting);
		StateMachine.Tick(Time.deltaTime);

		foreach (var ability in Moveset.AllAbilities)
		{
			ability.Tick(Time.deltaTime);
		}
	}

	private void OnDestroy()
	{
		StateMachine.Dispose();
		Moveset.Clear();
	}

	public void RequestCast(int id)
	{
		if (Moveset.Data == null) return;

		Buffer.Add(id);
	}

	public bool IsAbilityReady(int hashId)
	{
		var abilities = Moveset.GetAbilities(hashId);

		if (abilities == null || abilities.Count == 0)
		{
			return false;
		}

		return abilities[0].IsReady;
	}

	public bool CanExecuteNextAbility()
	{
		return StateMachine.CurrentState == null || CanEarlyCancel;
	}

	public void ExecuteAbility(int id)
	{
		CanEarlyCancel = false;
		Tracker.Advance(id);

		if (Tracker.CurrentAbility != null)
		{
			Tracker.CurrentAbility.Data.Execution.OnInputStarted(this, Tracker.CurrentAbility);
			Tracker.CurrentAbility.StartCooldown();
		}
	}

	public void StopAbility()
	{
		CanEarlyCancel = false;
		Tracker.Reset();
		StateMachine.Stop();
	}

	private void OnWeaponEquip(WeaponEquippedEvent e)
	{
		Moveset.UpdateMoveset(e.Data.Moveset);
	}
}
