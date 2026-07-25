using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class HitboxTracker : MonoBehaviour
{
    private Hitbox _activeHitbox;
    private LocalEventChannel _channel;

    private void Awake()
    {
        _channel = GetComponent<LocalEventChannel>();
    }

    private void OnEnable()
    {
        _channel.Subscribe<HitboxUpdateRequestedEvent>(OnHitboxUpdateRequested);
    }

    private void OnDisable()
    {
        _channel.Unsubscribe<HitboxUpdateRequestedEvent>(OnHitboxUpdateRequested);
    }

    private void OnHitboxUpdateRequested(HitboxUpdateRequestedEvent e)
    {
        _activeHitbox = e.hitbox;
    }

	public void EnableHitbox(AbilityEffectPayload payload)
	{
		if (_activeHitbox != null)
		{
			_activeHitbox.Enable(payload);
		}
	}

	public void DisableHitbox()
	{
		if (_activeHitbox != null)
		{
			_activeHitbox.Disable();
		}
	}

}
