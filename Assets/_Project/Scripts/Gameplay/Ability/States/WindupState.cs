using UnityEngine;

public class WindupState : AbilityState
{
	public WindupState(AbilityRunner runner) : base(runner) { }

	public override void OnEnter()
	{
		_runner.Channel.Publish(new AbilityCastStartedEvent(_runner.Tracker.CurrentAbility.AnimationHash, _runner.Tracker.CurrentAbility.AnimationSpeed));
		_timer.Start(_runner.Tracker.CurrentAbility.WindupDuration);
		Debug.Log($"WindupState: {_runner.Tracker.CurrentAbility.name} for {_runner.Tracker.CurrentAbility.WindupDuration} seconds");
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
		_runner.StateMachine.ChangeState<ExecutionState>();
	}
}
