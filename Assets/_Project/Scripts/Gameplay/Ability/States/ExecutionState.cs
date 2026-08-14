public class ExecutionState : AbilityState
{
	public ExecutionState(AbilityController controller) : base(controller)
	{
	}

	public override void OnEnter()
	{
		_timer.Start(_runner.Tracker.CurrentAbility.Data.ExecutionDuration);
		_runner.Tracker.CurrentAbility.StartExecute(_runner.gameObject);
	}

	public override void OnExit()
	{
		_runner.Tracker.CurrentAbility.EndExecute(_runner.gameObject);
	}

	public override void OnUpdate(float deltaTime)
	{
		_timer.Tick(deltaTime);
	}

	protected override void OnTimeUp()
	{
		_runner.StateMachine.ChangeState<RecoveryState>();
	}
}
