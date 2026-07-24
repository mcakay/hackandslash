using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Rigidbody))]
public class Hurtbox : MonoBehaviour
{
	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	public void ReceiveHit(HitPayload payload, Vector3 sourcePosition)
	{
		_channel.Publish(new EmissionRequestedEvent());
		_channel.Publish(new HitReceivedEvent(payload, sourcePosition));
	}
}
