using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Collider))]
public class Hurtbox : MonoBehaviour
{
	private LocalEventChannel _channel;
	private Collider _collider;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
		_collider = GetComponent<Collider>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	public void ReceiveHit(AbilityEffectPayload payload, Vector3 hitPosition)
	{
		_channel.Publish(new EmissionRequestedEvent());
		_channel.Publish(new HitReceivedEvent());
		payload.OnImpact(gameObject, hitPosition);
	}

	private void OnDeath(DeathEvent e)
	{
		_collider.enabled = false;
		this.enabled = false;
	}
}
