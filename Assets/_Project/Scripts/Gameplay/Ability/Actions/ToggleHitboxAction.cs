using System;
using UnityEngine;

[Serializable]
public class ToggleHitboxAction : AbilityAction
{
	public bool IsActive;

	public override void Execute(GameObject caster, AbilitySO context)
	{
		if (!caster.TryGetComponent(out HitboxTracker tracker))
		{
			return;
		}

		if (IsActive)
		{
			tracker.EnableHitbox(context.CreateEffectPayload(caster));
		}
		else
		{
			tracker.DisableHitbox();
		}
	}
}
