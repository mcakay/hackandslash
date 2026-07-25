using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
	private Collider _collider;
	private readonly HashSet<Hurtbox> _hitHurtboxes = new();

	private AbilityEffectPayload _payload;

	private void Awake()
	{
		_collider = GetComponent<Collider>();
		if (_collider)
		{
			_collider.isTrigger = true;
			_collider.enabled = false;
		}
	}

	public void Enable(AbilityEffectPayload payload)
	{
		_payload = payload;
		_hitHurtboxes.Clear();
		if (_collider != null)
		{
			_collider.enabled = true;
		}
	}
	public void Disable()
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

			if (_hitHurtboxes.Count == 1)
			{
				_payload.OnFirstImpact(gameObject, transform.position);
			}
		}
	}
}
