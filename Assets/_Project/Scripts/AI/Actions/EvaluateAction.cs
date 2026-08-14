using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EvaluateAction : IAction
{
	[SerializeField] private EntitySet targetEntities;
	[SerializeField] private EvaluationConfig config;

	public void Act(AIStateController controller)
	{
		if (!CanThink(controller)) return;
		Evaluate(controller, out Entity bestTarget, out Ability bestAbility, out float bestScore);
		ApplyDecision(controller, bestTarget, bestAbility, bestScore);

		if (bestTarget != null)
		{
			UpdateThinkTimer(controller);
		}
	}

	private bool CanThink(AIStateController controller)
	{
		if (controller.AbilityController == null) return false;
		if (Time.time < controller.NextThinkTime) return false;
		if (controller.AbilityController.StateMachine.CurrentState != null) return false;

		return true;
	}

	private void UpdateThinkTimer(AIStateController controller)
	{
		float randomVariance = UnityEngine.Random.Range(-config.IntervalVariance, config.IntervalVariance);
		controller.NextThinkTime = Time.time + config.ThinkInterval + randomVariance;
	}

	private void Evaluate(AIStateController controller, out Entity bestTarget, out Ability bestAbility, out float bestScore)
	{
		bestScore = -float.MaxValue;
		bestTarget = null;
		bestAbility = null;

		List<Entity> targets = targetEntities.Items;
		Dictionary<int, List<Ability>> abilities = controller.AbilityController.Moveset.AbilityLookup;

		for (var i = 0; i < targets.Count; i++)
		{
			Entity target = targets[i];

			if (!IsTargetInConsiderationRange(controller.transform.position, target.Transform.position))
				continue;

			foreach (var kvp in abilities)
			{
				List<Ability> comboList = kvp.Value;

				if (comboList == null || comboList.Count == 0)
					continue;

				Ability baseAbility = comboList[0];

				float currentScore = CalculateAbilityScore(controller, target, baseAbility);

				if (currentScore > bestScore)
				{
					bestScore = currentScore;
					bestTarget = target;
					bestAbility = baseAbility;
				}
			}
		}
	}

	private bool IsTargetInConsiderationRange(Vector3 aiPosition, Vector3 targetPosition)
	{
		float sqrDistanceToTarget = (targetPosition - aiPosition).sqrMagnitude;
		return sqrDistanceToTarget <= config.MaxConsiderationSqrDistance;
	}

	private float CalculateAbilityScore(AIStateController controller, Entity target, Ability ability)
	{
		float score = ability.Data.UtilityScore;

		if (target == controller.TargetEntity && ability == controller.SelectedAbility)
		{
			score += config.StickinessBonus;
		}

		var evaluators = ability.Data.Evaluators;
		for (int k = 0; k < evaluators.Count; k++)
		{
			score += evaluators[k].Evaluate(controller.Entity, target, ability);
		}

		score += UnityEngine.Random.Range(-config.ScoreNoise, config.ScoreNoise);

		return score;
	}

	private void ApplyDecision(AIStateController controller, Entity bestTarget, Ability bestAbility, float bestScore)
	{
		if (bestTarget != null && bestAbility != null)
		{
			controller.TargetEntity = bestTarget;
			controller.SelectedAbility = bestAbility;
		}
		else
		{
			controller.TargetEntity = null;
			controller.SelectedAbility = null;
		}
	}
}

