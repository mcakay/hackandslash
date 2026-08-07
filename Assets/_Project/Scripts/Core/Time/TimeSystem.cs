using UnityEngine;

public class TimeSystem : MonoBehaviour
{
	[SerializeField] private TimeScaleEventChannel timeScaleEventChannel;

	private Timer _hitStopTimer;
	private float _originalTimeScale = 1f;
	private bool _isHitStopping;

	private void Awake()
	{
		_hitStopTimer = new Timer();
	}

	private void OnEnable()
	{
		timeScaleEventChannel.Subscribe(OnTimeScaleEvent);
		_hitStopTimer.TimerEnded += OnHitStopFinished;
	}

	private void OnDisable()
	{
		timeScaleEventChannel.Unsubscribe(OnTimeScaleEvent);
		_hitStopTimer.TimerEnded -= OnHitStopFinished;
	}

	private void Update()
	{
		if (_hitStopTimer.IsRunning)
		{
			_hitStopTimer.Tick(Time.unscaledDeltaTime);
		}
	}

	public void OnTimeScaleEvent(TimeScaleEventPayload payload)
	{
		if (!_isHitStopping)
		{
			_originalTimeScale = Time.timeScale;
		}

		_isHitStopping = true;
		Time.timeScale = payload.TimeScale;
		_hitStopTimer.Start(payload.Duration);
	}

	private void OnHitStopFinished()
	{
		Time.timeScale = _originalTimeScale;
		_isHitStopping = false;
	}
}
