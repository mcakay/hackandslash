using UnityEngine;

public class RecoveryState : AbilityState
{
	public RecoveryState(AbilityController controller) : base(controller)
	{
	}

	public override void OnEnter()
	{
		_timer.Start(_runner.Tracker.CurrentAbility.Data.RecoveryDuration);
		_runner.CanEarlyCancel = true;
	}

	public override void OnExit()
	{
		_runner.CanEarlyCancel = false;
		_runner.Channel.Publish(new AbilityCastEndedEvent());
	}

	public override void OnUpdate(float deltaTime)
	{
		_timer.Tick(deltaTime);
	}

	protected override void OnTimeUp()
	{
		_runner.StopAbility();
	}
}
