using System;
using UnityEngine;

public class AnimationVariant : IDisposable
{
	private readonly AnimationVariantData _data;
	private readonly int _hash;
	private float _targetValue;

	private readonly Timer _timer;
	private readonly Animator _animator;

	public AnimationVariant(AnimationVariantData data, Animator animator)
	{
		_data = data;
		_animator = animator;
		_hash = Animator.StringToHash(_data.ParameterName);

		if (_data.AutoChangeOverTime)
		{
			_timer = new Timer();

			_timer.TimerEnded += OnTimerEnded;
		}
		PickRandomValue();
	}

	public void Tick(float deltaTime)
	{
		_timer?.Tick(deltaTime);

		if (_data.TransitionSpeed > 0)
		{
			_animator.SetFloat(_hash, _targetValue, _data.TransitionSpeed, deltaTime);
		}
	}

	public void Dispose()
	{
		if (_timer == null) return;
		_timer.TimerEnded -= OnTimerEnded;
	}

	private void OnTimerEnded()
	{
		PickRandomValue();
	}

	private void PickRandomValue()
	{
		if (_data.PossibleValues.Length == 0) return;

		int index = UnityEngine.Random.Range(0, _data.PossibleValues.Length);
		_targetValue = _data.PossibleValues[index];

		if (_data.TransitionSpeed <= 0)
		{
			_animator.SetFloat(_hash, _targetValue);
		}

		if (_data.AutoChangeOverTime && _timer != null)
		{
			float randomTime = UnityEngine.Random.Range(_data.MinTime, _data.MaxTime);
			_timer.Start(randomTime);
		}
	}
}
