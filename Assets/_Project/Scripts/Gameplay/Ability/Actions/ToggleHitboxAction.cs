using System;
using UnityEngine;

[Serializable]
public class ToggleHitboxAction : AbilityAction
{
	public bool IsActive;
	public string HitboxId;

	public override void Execute(GameObject caster, AbilitySO context)
	{
		if (!caster.TryGetComponent(out HitboxController controller))
		{
			return;
		}

		if (IsActive)
		{
			controller.EnableHitbox(HitboxId, context.CreateEffectPayload(caster));
		}
		else
		{
			controller.DisableHitbox(HitboxId);
		}
	}
}
