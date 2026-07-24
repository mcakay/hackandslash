using UnityEngine;

[CreateAssetMenu(fileName = "Melee Ability", menuName = "Data/Abilities/Melee Ability")]
public class MeleeAbilitySO : AbilitySO
{

	public float Damage = 10f;

	public override void StartExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(true, new HitPayload(Damage, KnockbackForce, ImpactSFX)));
	}

	public override void EndExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(false, default(HitPayload)));
	}
}
