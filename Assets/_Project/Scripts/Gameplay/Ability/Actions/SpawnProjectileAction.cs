using System;
using UnityEngine;

[Serializable]
public class SpawnProjectileAction : AbilityAction
{
	public string Id;
	public ProjectileFactorySO ProjectileFactory;

	public override void Execute(GameObject caster, Ability ability)
	{
		if (caster.TryGetComponent(out ProjectileController controller))
        {
            Transform spawnPoint = controller.GetSpawnPoint(Id);
            AbilityEffectPayload payload = ability.CreateEffectPayload(caster);

            Projectile proj = ProjectileFactory.Get(spawnPoint.position, spawnPoint.rotation);

            proj.Fire(caster, payload);
        }
	}
}
