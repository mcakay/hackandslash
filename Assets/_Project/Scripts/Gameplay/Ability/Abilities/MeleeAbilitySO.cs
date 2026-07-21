using UnityEngine;

[CreateAssetMenu(fileName = "Melee Ability", menuName = "Data/Abilities/Melee Ability")]
public class MeleeAbilitySO : AbilitySO
{
	public override void StartExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(true));
	}

	public override void EndExecute(GameObject caster, LocalEventChannel channel)
	{
		channel.Publish(new HitboxStateRequestedEvent(false));
	}
}
