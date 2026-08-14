using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Entity))]
public class Hurtbox : MonoBehaviour
{
	private LocalEventChannel _channel;

	public Entity Entity { get; private set; }

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
		Entity = GetComponent<Entity>();
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
