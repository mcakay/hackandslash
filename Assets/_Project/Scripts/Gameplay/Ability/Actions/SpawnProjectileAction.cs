using System;
using UnityEngine;

[Serializable]
public class SpawnProjectileAction : AbilityAction
{
	public string Id;

	public Projectile Prefab;
	public float Speed = 20f;

	public override void Execute(GameObject caster, AbilitySO context)
	{
		if (caster.TryGetComponent(out ProjectileController controller))
		{
			Transform spawnPoint = controller.GetSpawnPoint(Id);

			AbilityEffectPayload payload = context.CreateEffectPayload(caster);

			Projectile proj = UnityEngine.Object.Instantiate(Prefab, spawnPoint.position, spawnPoint.rotation);
			proj.Fire(caster, payload, Speed, caster.transform.forward);
		}
	}
}
