using System;
using UnityEngine;

[Serializable]
public class CooldownEvaluator : IEvaluator
{
    public float Weight = 50f;

    public AnimationCurve ScoreByRemainingTime;

    public float Evaluate(Entity caster, Entity target, Ability ability)
    {
        float remainingTime = ability.IsReady ? 0f : ability.CooldownRemaining;

        return ScoreByRemainingTime.Evaluate(remainingTime) * Weight;
    }
}
