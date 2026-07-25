using System;
using UnityEngine;

[Serializable]
public abstract class AbilityEffect
{
    public abstract void Execute(GameObject caster, GameObject target, Vector3 position);
}
