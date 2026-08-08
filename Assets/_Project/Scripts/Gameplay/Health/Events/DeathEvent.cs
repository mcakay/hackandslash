using UnityEngine;

public readonly struct DeathEvent : ILocalEvent
{
	public readonly Vector3 HitPosition;
	public readonly Vector3 HitDirection;
	public readonly float ExcessDamage;

	public DeathEvent(Vector3 hitPosition, Vector3 hitDirection, float excessDamage)
	{
		HitPosition = hitPosition;
		HitDirection = hitDirection;
		ExcessDamage = excessDamage;
	}
}
