using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Collider))]
public class Hurtbox : MonoBehaviour
{
	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	public void ReceiveHit(AbilityEffectPayload payload, Vector3 hitPosition, GameObject source)
	{
		_channel.Publish(new EmissionRequestedEvent());
		_channel.Publish(new HitReceivedEvent());
		payload.OnImpact(source, gameObject, hitPosition);
	}

	private void OnDeath(DeathEvent e)
	{
		this.enabled = false;
	}
}
