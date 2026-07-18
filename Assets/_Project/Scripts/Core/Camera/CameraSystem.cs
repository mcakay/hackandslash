using Unity.Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private CinemachineCamera vCam;

	private CinemachineBasicMultiChannelPerlin _noiseProfile;

	private Transform _defaultFollowTarget;
	private Transform _defaultLookAtTarget;

	private readonly Timer _shakeTimer = new();
	private readonly Timer _zoomTimer = new();
	private readonly Timer _focusTimer = new();

	private float _startFOV;
	private float _targetFOV;

	private void Awake()
	{
		if (vCam != null)
		{
			_noiseProfile = vCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
			_defaultFollowTarget = vCam.Follow;
			_defaultLookAtTarget = vCam.LookAt;

			if (_noiseProfile != null) _noiseProfile.AmplitudeGain = 0f;
		}
	}

	private void OnEnable()
	{
		_shakeTimer.TimerEnded += OnShakeTimerEnded;
		_zoomTimer.TimerEnded += OnZoomTimerEnded;
		_focusTimer.TimerEnded += OnFocusTimerEnded;
	}

	private void OnDisable()
	{
		_shakeTimer.TimerEnded -= OnShakeTimerEnded;
		_zoomTimer.TimerEnded -= OnZoomTimerEnded;
		_focusTimer.TimerEnded -= OnFocusTimerEnded;
	}

	private void OnShakeTimerEnded()
	{
		if (_noiseProfile != null) _noiseProfile.AmplitudeGain = 0f;
	}

	private void OnZoomTimerEnded()
	{
		vCam.Lens.OrthographicSize = _targetFOV;
	}

	private void OnFocusTimerEnded()
	{
		vCam.Follow = _defaultFollowTarget;
		vCam.LookAt = _defaultLookAtTarget;
	}

	private void Update()
	{
		float dt = Time.deltaTime;

		_shakeTimer.Tick(dt);
		_zoomTimer.Tick(dt);
		_focusTimer.Tick(dt);

		if (_zoomTimer.IsRunning)
		{
			vCam.Lens.OrthographicSize = Mathf.Lerp(_startFOV, _targetFOV, _zoomTimer.Progress);
		}
	}

	public void OnCameraShake(ShakePayload payload)
	{
		if (_noiseProfile == null) return;
		_noiseProfile.AmplitudeGain = payload.Intensity;
		_shakeTimer.Start(payload.Duration);
	}

	public void OnCameraZoom(ZoomPayload payload)
	{
		_startFOV = vCam.Lens.OrthographicSize;
		_targetFOV = payload.TargetFOV;
		_zoomTimer.Start(payload.Duration);
	}

	public void OnCameraFocus(FocusPayload payload)
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
