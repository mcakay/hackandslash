using UnityEngine;

public class RecoveryState : AbilityState
{
	private float _earlyCancelTime;

	public RecoveryState(AbilityRunner runner) : base(runner)
	{
	}

	public override void OnEnter()
	{
		_timer.Start(_runner.Tracker.CurrentAbility.RecoveryDuration);
		_earlyCancelTime = _runner.Tracker.CurrentAbility.CancelDuration;

		Debug.Log($"RecoveryState: {_runner.Tracker.CurrentAbility.name} for {_runner.Tracker.CurrentAbility.RecoveryDuration} seconds");
	}

	public override void OnExit()
	{
		_runner.CanEarlyCancel = false;
	}

	public override void OnUpdate(float deltaTime)
	{
		_timer.Tick(deltaTime);

		if (_timer.ElapsedTime >= _earlyCancelTime && !_runner.CanEarlyCancel)
		{
			_runner.CanEarlyCancel = true;
		}
	}

	protected override void OnTimeUp()
	{
		_runner.StopAbility();
	}
}
