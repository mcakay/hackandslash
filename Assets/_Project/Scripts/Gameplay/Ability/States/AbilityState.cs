using System;

public abstract class AbilityState : IState, IDisposable
{
	protected AbilityController _runner;
	protected readonly Timer _timer = new();

	public AbilityState(AbilityController controller)
	{
		_runner = controller;
		_timer.TimerEnded += OnTimeUp;
	}

	public abstract void OnEnter();
	public abstract void OnExit();
	public abstract void OnUpdate(float deltaTime);
	protected abstract void OnTimeUp();

	public void Dispose()
	{
		_timer.TimerEnded -= OnTimeUp;
	}
}
