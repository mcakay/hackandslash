using UnityEngine;

public class WindupState : AbilityState
{
	public WindupState(AbilityController controller) : base(controller) { }

	public override void OnEnter()
	{
		_runner.Channel.Publish(new AbilityCastStartedEvent(_runner.Tracker.CurrentAbility, _runner.Tracker.CurrentAbility.Data.AnimationSpeed));
		_timer.Start(_runner.Tracker.CurrentAbility.Data.WindupDuration);
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
