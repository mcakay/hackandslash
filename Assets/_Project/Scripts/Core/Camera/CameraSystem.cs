using Unity.Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private CinemachineCamera vCam;
	[SerializeField] private CinemachineImpulseSource impulseSource;

	private Transform _defaultFollowTarget;
	private Transform _defaultLookAtTarget;

	private readonly Timer _zoomTimer = new();
	private readonly Timer _focusTimer = new();

	private float _startFOV;
	private float _targetFOV;

	private void Awake()
	{
		if (vCam != null)
		{
			_defaultFollowTarget = vCam.Follow;
			_defaultLookAtTarget = vCam.LookAt;
		}
	}

	private void OnEnable()
	{
		_zoomTimer.TimerEnded += OnZoomTimerEnded;
		_focusTimer.TimerEnded += OnFocusTimerEnded;
	}

	private void OnDisable()
	{
		_zoomTimer.TimerEnded -= OnZoomTimerEnded;
		_focusTimer.TimerEnded -= OnFocusTimerEnded;
	}

	private void OnZoomTimerEnded()
	{
		vCam.Lens.FieldOfView = _startFOV;
	}

	private void OnFocusTimerEnded()
	{
		vCam.Follow = _defaultFollowTarget;
		vCam.LookAt = _defaultLookAtTarget;
	}

	private void Update()
	{
		float dt = Time.deltaTime;

		_zoomTimer.Tick(dt);
		_focusTimer.Tick(dt);

		if (_zoomTimer.IsRunning)
		{
			float t = Mathf.PingPong(_zoomTimer.Progress * 2f, 1f);
			vCam.Lens.FieldOfView = Mathf.Lerp(_startFOV, _targetFOV, t);
		}
	}

	public void OnCameraShake(ShakeEventPayload payload)
	{
		if (impulseSource == null) return;

		impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = payload.Duration * 0.2f;
		impulseSource.ImpulseDefinition.TimeEnvelope.DecayTime = payload.Duration * 0.8f;

		impulseSource.GenerateImpulse(payload.Intensity);
	}

	public void OnCameraZoom(ZoomEventPayload payload)
	{
		_startFOV = vCam.Lens.FieldOfView;

		_targetFOV = _startFOV - payload.Amount;

		_zoomTimer.Start(payload.Duration);
	}

	public void OnCameraFocus(FocusEventPayload payload)
	{
		if (!_focusTimer.IsRunning)
		{
			_defaultFollowTarget = vCam.Follow;
			_defaultLookAtTarget = vCam.LookAt;
		}

		vCam.Follow = payload.Target;
		vCam.LookAt = payload.Target;
		_focusTimer.Start(payload.Duration);
	}
}
