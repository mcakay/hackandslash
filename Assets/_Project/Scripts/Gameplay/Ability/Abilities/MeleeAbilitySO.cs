using UnityEngine;

[CreateAssetMenu(fileName = "Melee Ability", menuName = "Data/Abilities/Melee Ability")]
public class MeleeAbilitySO : AbilitySO
{
	public AudioClip swingSFX;
	public AudioClip hitSFX;
	public float Damage = 10f;

	public override void StartExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(true, Damage, KnockbackForce, swingSFX, hitSFX));
	}

	public override void EndExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(false, 0f, 0f, null, null));
	}
}
