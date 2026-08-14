using System;
using UnityEngine;

[Serializable]
public abstract class AbilityAction
{
	public abstract void Execute(GameObject caster, Ability ability);
}
