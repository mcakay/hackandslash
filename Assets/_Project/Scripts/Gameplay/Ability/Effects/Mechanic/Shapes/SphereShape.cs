using System;
using UnityEngine;

[Serializable]
public class SphereShape: AbilityShape
{
	public float Radius;

	public override int GetTargets(Vector3 origin, Vector3 direction, Collider[] results)
	{
		return Physics.OverlapSphereNonAlloc(origin, Radius, results);
	}
}
