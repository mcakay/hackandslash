using System;
using UnityEngine;

[Serializable]
public class DistanceEvaluator : IEvaluator
{
	public float Weight = 50f;
	public AnimationCurve ScoreByDistance;

	public float Evaluate(Entity caster, Entity target, Ability ability)
	{
		float distance = Vector3.Distance(caster.Transform.position, target.Transform.position);
		float normalizedDistance = Mathf.Clamp01(distance / ability.Data.Range);

		return ScoreByDistance.Evaluate(normalizedDistance) * Weight;
	}
}
