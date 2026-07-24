using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class PlayerFeedbackController : MonoBehaviour
{
	[SerializeField] private ShakeEventSO shakeEvent;
	[SerializeField] private ZoomEventSO zoomEvent;

	private LocalEventChannel _channel;

	private AbilitySO _currentAbility;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<FirstImpactRegisteredEvent>(OnFirstImpactRegistered);
		_channel.Subscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<FirstImpactRegisteredEvent>(OnFirstImpactRegistered);
		_channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void OnFirstImpactRegistered(FirstImpactRegisteredEvent e)
	{
		if (_currentAbility == null)
		{
			return;
		}
		if (_currentAbility.IsScreenShake)
		{
			shakeEvent.Raise(new ShakeEventPayload(_currentAbility.ScreenShakeIntensity, _currentAbility.ScreenShakeDuration));
		}

		if (_currentAbility.IsFovZoom)
		{
			zoomEvent.Raise(new ZoomEventPayload(_currentAbility.FovZoomAmount, _currentAbility.FovZoomDuration));
		}
	}

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		_currentAbility = e.Ability;
	}
}
