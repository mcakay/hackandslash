using System;
using UnityEngine;

[Serializable]
public class DamageEffect : MechanicEffect
{
	public float Damage = 10f;

	public override void Execute(GameObject caster, GameObject source, GameObject target, Vector3 position)
	{
		if (target != null && target.TryGetComponent(out IDamageable damageable))
		{
			damageable.TakeDamage(Damage, position, source.transform.forward);
		}
	}
}
