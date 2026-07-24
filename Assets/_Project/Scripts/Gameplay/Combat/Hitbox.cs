using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
	[SerializeField] private SFXEventSO SFXEvent;

	private Collider _collider;
	private LocalEventChannel _channel;
	private readonly HashSet<Hurtbox> _hitHurtboxes = new();

	private HitPayload _payload;

	private void Awake()
	{
		_collider = GetComponent<Collider>();
		if (_collider)
		{
			_collider.isTrigger = true;
			_collider.enabled = false;
		}
	}

	public void Initialize(LocalEventChannel channel)
	{
		_channel = channel;
	}

	public void EnableHitbox(HitPayload payload)
	{
		_payload = payload;

		_hitHurtboxes.Clear();
		if (_collider != null)
		{
			_collider.enabled = true;
		}
	}

	public void DisableHitbox()
	{
		if (_collider != null)
		{
			_collider.enabled = false;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Hurtbox hurtbox))
		{
			if (!_hitHurtboxes.Add(hurtbox))
			{
				return;
			}

			hurtbox.ReceiveHit(_payload, transform.position);

			if (_hitHurtboxes.Count == 1 && _channel != null)
			{
				_channel.Publish(new FirstImpactRegisteredEvent(_payload.ImpactSFX, transform.position));
			}
		}
	}
}
