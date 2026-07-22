using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
	[SerializeField] private SFXEventSO SFXEvent;

	private Collider _collider;

	private LocalEventChannel _channel;
	private readonly HashSet<Hurtbox> _hitHurtboxes = new();

	private float _damage;
	private float _knockbackForce;
	private AudioClip _hitSFX;

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

	public void EnableHitbox(float damage, float knockbackForce, AudioClip hitSFX)
	{
		_damage = damage;
		_knockbackForce = knockbackForce;
		_hitSFX = hitSFX;

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
			if (_hitHurtboxes.Add(hurtbox))
			{
				hurtbox.Damage(_damage, _knockbackForce, transform.position);

				if (_hitHurtboxes.Count == 1 && _channel != null)
				{
					_channel.Publish(new FirstHitRegisteredEvent());
					SFXEvent.Raise(new SFXEventPayload(_hitSFX, 1f, Random.Range(0.9f, 1.1f), transform.position));
				}
			}
		}
	}
}
