using System.Collections.Generic;

public readonly struct HitPayload
{
    public readonly float Damage;
	public readonly float KnockbackForce;
	public readonly List<SFXConfig> ImpactSFX;

	public HitPayload(float damage, float knockbackForce, List<SFXConfig> impactSFX)
	{
		Damage = damage;
		KnockbackForce = knockbackForce;
		ImpactSFX = impactSFX;
	}
}
