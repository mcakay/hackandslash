using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class MechanicEffect : AbilityEffect
{
	public List<TagSO> ExcludeTags;
	public List<TagSO> IncludeTags;

	public bool CanApplyTo(GameObject target)
	{
		if (target != null && target.TryGetComponent(out TagController tags))
		{
			if (tags.HasAnyTag(ExcludeTags)) return false;
			if (!tags.HasAllTags(IncludeTags)) return false;
		}

		return true;
	}
}
