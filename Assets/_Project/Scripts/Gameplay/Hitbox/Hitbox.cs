using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hitbox : MonoBehaviour
{
	private Collider _collider;

	private readonly HashSet<Hurtbox> _hitHurtboxes = new();

	private void Awake()
	{
		_collider = GetComponent<Collider>();
		if (_collider)
		{
			_collider.isTrigger = true;
			_collider.enabled = false;
		}
	}

	public void EnableHitbox()
	{
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
			if (!_hitHurtboxes.Contains(hurtbox))
			{
				Debug.Log($"Hitbox: {name} hit Hurtbox: {hurtbox.name}");
			}
		}
	}
}
