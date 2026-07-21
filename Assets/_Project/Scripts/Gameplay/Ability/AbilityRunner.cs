using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class AbilityRunner : MonoBehaviour
{
	[SerializeField] private AbilityConfig config;
	[SerializeField] private MovesetSO moveset;

	private AbilityBuffer _buffer;
	public AbilityTracker Tracker { get; private set; }
	public StateMachine StateMachine { get; private set; }

	public LocalEventChannel Channel { get; private set;}

	public bool CanEarlyCancel { get; set; } = false;

	private void Awake()
	{
		StateMachine = new StateMachine();
		_buffer = new AbilityBuffer(config, CanExecuteNextAbility, ExecuteAbility);
		Tracker = new AbilityTracker();

		StateMachine.AddState(new WindupState(this));
		StateMachine.AddState(new ExecutionState(this));
		StateMachine.AddState(new RecoveryState(this));

		Channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		Channel.Subscribe<MovesetUpdateRequestedEvent>(OnMovesetUpdateRequested);
		Channel.Subscribe<AbilityCastRequestedEvent>(OnCastRequested);
	}

	private void OnDisable()
	{
		Channel.Unsubscribe<MovesetUpdateRequestedEvent>(OnMovesetUpdateRequested);
		Channel.Unsubscribe<AbilityCastRequestedEvent>(OnCastRequested);
	}

	private void Update()
	{
		bool isExecuting = StateMachine.CurrentState != null;

		_buffer.Process();
		Tracker.Tick(isExecuting);
		StateMachine.Tick(Time.deltaTime);
	}

	private void OnMovesetUpdateRequested(MovesetUpdateRequestedEvent e)
	{
		if (e.moveset == null || e.moveset == moveset)
		{
			return;
		}
		moveset = e.moveset;
		moveset.Initialize();
		Tracker.Reset();
	}

	private void OnCastRequested(AbilityCastRequestedEvent e)
	{
		if (moveset == null)
		{
			return;
		}
		_buffer.Add(e.Id);
	}

	private bool CanExecuteNextAbility()
	{
		return StateMachine.CurrentState == null || CanEarlyCancel;
	}

	private void ExecuteAbility(int id)
	{
		CanEarlyCancel = false;

		Tracker.Advance(id, moveset);

		if (Tracker.CurrentAbility != null)
		{
			StateMachine.ChangeState<WindupState>();
		}
	}

	public void StopAbility()
	{
		CanEarlyCancel = false;

		Tracker.Reset();
		StateMachine.Stop();
	}
}
