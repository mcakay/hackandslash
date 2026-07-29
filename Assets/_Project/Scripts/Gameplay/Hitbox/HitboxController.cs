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
        _channel.Subscribe<HitboxRegisterRequestedEvent>(OnHitboxRegisterRequested);
    }

    private void OnDisable()
    {
        _channel.Unsubscribe<HitboxRegisterRequestedEvent>(OnHitboxRegisterRequested);
    }

    private void OnHitboxRegisterRequested(HitboxRegisterRequestedEvent e)
    {
        _hitboxes[e.hitbox.Id] = e.hitbox;
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
}
