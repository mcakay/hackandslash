using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class HitboxTracker : MonoBehaviour
{
	[SerializeField] private SFXEventSO SFXEvent;

	private Hitbox _activeHitbox;
	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<HitboxUpdateRequestedEvent>(OnHitboxUpdateRequested);
		_channel.Subscribe<HitboxStateRequestedEvent>(OnHitboxStateRequested);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<HitboxUpdateRequestedEvent>(OnHitboxUpdateRequested);
		_channel.Unsubscribe<HitboxStateRequestedEvent>(OnHitboxStateRequested);
	}

	private void OnHitboxUpdateRequested(HitboxUpdateRequestedEvent e)
	{
		_activeHitbox = e.hitbox;
		_activeHitbox.Initialize(_channel);
	}

	private void OnHitboxStateRequested(HitboxStateRequestedEvent e)
	{
		if (e.IsActive)
		{
			_activeHitbox.EnableHitbox(e.Damage, e.KnockbackForce, e.HitSFX);
			SFXEvent.Raise(new SFXPayload(e.SwingSFX, 1f, Random.Range(0.9f, 1.1f), transform.position));
		}
		else
		{
			_activeHitbox.DisableHitbox();
		}
	}
}
