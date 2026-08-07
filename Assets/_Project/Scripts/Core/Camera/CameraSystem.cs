using Unity.Cinemachine;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private CinemachineCamera vCam;
	[SerializeField] private CinemachineImpulseSource impulseSource;

	[Header("Events (Listening)")]
	[SerializeField] private CameraZoomEventChannel zoomEventChannel;
	[SerializeField] private CameraImpulseEventChannel impulseEventChannel;

	private readonly Timer _zoomTimer = new();

	private float _startFOV;
	private float _targetFOV;

	private void OnEnable()
	{
		_zoomTimer.TimerEnded += OnZoomTimerEnded;

		if (zoomEventChannel != null)
		{
			zoomEventChannel.Subscribe(OnCameraZoomEvent);
		}

		if (impulseEventChannel != null)
		{
			impulseEventChannel.Subscribe(OnCameraImpulseEvent);
		}
	}

	private void OnDisable()
	{
		_zoomTimer.TimerEnded -= OnZoomTimerEnded;

		if (zoomEventChannel != null)
		{
			zoomEventChannel.Unsubscribe(OnCameraZoomEvent);
		}

		if (impulseEventChannel != null)
		{
			impulseEventChannel.Unsubscribe(OnCameraImpulseEvent);
		}
	}

	private void OnZoomTimerEnded()
	{
		vCam.Lens.FieldOfView = _startFOV;
	}

	private void Update()
	{
		float dt = Time.deltaTime;

		_zoomTimer.Tick(dt);

		if (_zoomTimer.IsRunning)
		{
			float t = Mathf.PingPong(_zoomTimer.Progress * 2f, 1f);
			vCam.Lens.FieldOfView = Mathf.Lerp(_startFOV, _targetFOV, t);
		}
	}

	public void OnCameraImpulseEvent(CameraImpulseEventPayload payload)
	{
		if (impulseSource == null) return;

		impulseSource.ImpulseDefinition.TimeEnvelope.SustainTime = payload.Duration * 0.2f;
		impulseSource.ImpulseDefinition.TimeEnvelope.DecayTime = payload.Duration * 0.8f;

		impulseSource.GenerateImpulse(payload.Intensity);
	}

	public void OnCameraZoomEvent(CameraZoomEventPayload payload)
	{
		_startFOV = vCam.Lens.FieldOfView;

		_targetFOV = _startFOV - payload.Amount;

		_zoomTimer.Start(payload.Duration);
	}
}
