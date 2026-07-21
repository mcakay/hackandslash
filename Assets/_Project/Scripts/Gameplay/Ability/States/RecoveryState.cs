using UnityEngine;

public class RecoveryState : AbilityState
{
	public RecoveryState(AbilityRunner runner) : base(runner)
	{
	}

	public override void OnEnter()
	{
		_timer.Start(_runner.Tracker.CurrentAbility.RecoveryDuration);
		_runner.CanEarlyCancel = true;

		Debug.Log($"RecoveryState: {_runner.Tracker.CurrentAbility.name} for {_runner.Tracker.CurrentAbility.RecoveryDuration} seconds");
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
