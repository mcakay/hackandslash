using System;

public class Timer
{
	public event Action TimerEnded;

	private float _duration;
	private float _remainingTime;

	public bool IsRunning { get; private set; }

	public float Progress => _duration > 0f ? 1f - (_remainingTime / _duration) : 0f;

	public void Start(float duration)
	{
		_duration = duration;
		_remainingTime = duration;
		IsRunning = true;
	}

	public void Stop()
	{
		IsRunning = false;
		_remainingTime = 0f;
	}

	public void Pause()
	{
		IsRunning = false;
	}

	public void Resume()
	{
		if (_remainingTime > 0f)
		{
			IsRunning = true;
		}
	}

	public void Tick(float deltaTime)
	{
		if (!IsRunning)
		{
			return;
		}

		_remainingTime -= deltaTime;

		if (_remainingTime <= 0f)
		{
			IsRunning = false;
			_remainingTime = 0f;
			TimerEnded?.Invoke();
		}
	}
}
