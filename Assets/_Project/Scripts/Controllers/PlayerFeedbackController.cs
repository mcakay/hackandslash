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
		_channel.Subscribe<FirstHitRegisteredEvent>(OnFirstHitRegistered);
		_channel.Subscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<FirstHitRegisteredEvent>(OnFirstHitRegistered);
		_channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void OnFirstHitRegistered(FirstHitRegisteredEvent e)
	{
		if (_currentAbility == null)
		{
			return;
		}
		if (_currentAbility.IsScreenShake)
		{
			shakeEvent.Raise(new ShakePayload(_currentAbility.ScreenShakeIntensity, _currentAbility.ScreenShakeDuration));
		}

		if (_currentAbility.IsFovZoom)
		{
			zoomEvent.Raise(new ZoomPayload(_currentAbility.FovZoomAmount, _currentAbility.FovZoomDuration));
		}
	}

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		_currentAbility = e.Ability;
	}
}
