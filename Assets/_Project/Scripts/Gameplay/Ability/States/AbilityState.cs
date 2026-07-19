public abstract class AbilityState : IState
{
	protected AbilityRunner _runner;
	protected readonly Timer _timer = new();

	public AbilityState(AbilityRunner runner)
	{
		_runner = runner;
		_timer.TimerEnded += OnTimeUp;
	}

	public abstract void OnEnter();
	public abstract void OnExit();
	public abstract void OnUpdate(float deltaTime);
	protected abstract void OnTimeUp();
}
