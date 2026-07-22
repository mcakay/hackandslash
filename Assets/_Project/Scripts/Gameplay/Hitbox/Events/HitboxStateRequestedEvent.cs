using UnityEngine;

public readonly struct HitboxStateRequestedEvent : ILocalEvent
{
	public readonly bool IsActive;
	public readonly float Damage;
	public readonly float KnockbackForce;

	public readonly AudioClip SwingSFX;
	public readonly AudioClip HitSFX;

	public HitboxStateRequestedEvent(bool isActive, float damage, float knockbackForce, AudioClip swingSFX, AudioClip hitSFX)
	{
		IsActive = isActive;
		Damage = damage;
		KnockbackForce = knockbackForce;
		SwingSFX = swingSFX;
		HitSFX = hitSFX;
	}
}
