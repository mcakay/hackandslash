using System;
using UnityEngine;

[Serializable]
public class ToggleHitboxAction : AbilityAction
{
	public bool IsActive;
	public string HitboxId;

	public override void Execute(GameObject caster, Ability ability)
	{
		if (!caster.TryGetComponent(out HitboxController controller))
		{
			return;
		}

		if (IsActive)
		{
			controller.EnableHitbox(HitboxId, ability.CreateEffectPayload(caster));
		}
		else
		{
			controller.DisableHitbox(HitboxId);
		}
	}
}
