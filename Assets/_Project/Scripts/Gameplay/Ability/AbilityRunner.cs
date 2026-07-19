using UnityEngine;

public class AbilityRunner : MonoBehaviour
{
	[SerializeField] private AbilityConfig config;
	[SerializeField] private MovesetSO moveset;

	private AbilityBuffer _buffer;
	public AbilityTracker Tracker { get; private set; }
	public StateMachine StateMachine { get; private set; }

	public bool CanEarlyCancel { get; set; } = false;

	private void Awake()
	{
		StateMachine = new StateMachine();
		_buffer = new AbilityBuffer(config, CanExecuteNextAbility, ExecuteAbility);
		Tracker = new AbilityTracker();

		StateMachine.AddState(new WindupState(this));
		StateMachine.AddState(new ExecutionState(this));
		StateMachine.AddState(new RecoveryState(this));
	}

	private void Update()
	{
		bool isExecuting = StateMachine.CurrentState != null;

		_buffer.Process();
		Tracker.Tick(isExecuting);
		StateMachine.Tick(Time.deltaTime);
	}

	public void SetMoveset(MovesetSO newMoveset)
	{
		if (newMoveset == null || newMoveset == moveset)
		{
			return;
		}
		moveset = newMoveset;
		moveset.Initialize();
		Tracker.Reset();
	}

	public void BufferAbility(int id)
	{
		_buffer.Add(id);
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
