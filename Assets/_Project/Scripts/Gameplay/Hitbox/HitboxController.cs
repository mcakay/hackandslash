using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class HitboxController : MonoBehaviour
{
	private readonly Dictionary<string, Hitbox> _hitboxes = new();
	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<WeaponEquippedEvent>(OnWeaponEquipped);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<WeaponEquippedEvent>(OnWeaponEquipped);
	}

	public void EnableHitbox(string hitboxId, AbilityEffectPayload payload)
	{
		if (_hitboxes.TryGetValue(hitboxId, out Hitbox hitbox))
		{
			hitbox.Enable(payload);
		}
	}

	public void DisableHitbox(string hitboxId)
	{
		if (_hitboxes.TryGetValue(hitboxId, out Hitbox hitbox))
		{
			hitbox.Disable();
		}
	}

	private void OnWeaponEquipped(WeaponEquippedEvent e)
	{
		AddHitbox(e.Hitbox);
	}

	private void AddHitbox(Hitbox hitbox)
	{
		if (hitbox != null && !_hitboxes.ContainsKey(hitbox.Id))
		{
			_hitboxes[hitbox.Id] = hitbox;
		}
	}
}
