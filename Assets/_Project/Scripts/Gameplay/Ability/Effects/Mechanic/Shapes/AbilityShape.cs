using System;
using UnityEngine;

[Serializable]
public abstract class AbilityShape
{
    public abstract int GetTargets(Vector3 origin, Vector3 direction, Collider[] results);
}
