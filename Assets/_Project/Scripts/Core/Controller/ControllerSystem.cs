using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerFeedbackSystem : MonoBehaviour
{
    [Header("Rumble State")]
    private bool _isRumbling;
    private Timer _rumbleTimer;

	private void Awake()
	{
		_rumbleTimer = new Timer();
	}

	private void OnEnable()
	{
		_rumbleTimer.TimerEnded += StopRumble;
	}

	private void OnDisable()
    {
		_rumbleTimer.TimerEnded -= StopRumble;
		StopRumble();
    }

    private void Update()
    {
        if (_isRumbling)
        {
			_rumbleTimer.Tick(Time.unscaledDeltaTime);
		}
    }

    public void OnControllerHapticEvent(ControllerHapticEventPayload payload)
    {
        if (Gamepad.current == null) return;

        Gamepad.current.SetMotorSpeeds(payload.LowFrequency, payload.HighFrequency);

        _isRumbling = true;
    }

    private void StopRumble()
    {
        _isRumbling = false;

        Gamepad.current?.SetMotorSpeeds(0f, 0f);
    }
}
