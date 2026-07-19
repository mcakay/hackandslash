using UnityEngine;

public class ExecutionState : AbilityState
{
	public ExecutionState(AbilityRunner runner) : base(runner)
	{
	}

	public override void OnEnter()
	{
		_timer.Start(_runner.Tracker.CurrentAbility.ExecutionDuration);
		Debug.Log($"ExecutionState: {_runner.Tracker.CurrentAbility.name} for {_runner.Tracker.CurrentAbility.ExecutionDuration} seconds");
	}

	public override void OnExit()
	{
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
