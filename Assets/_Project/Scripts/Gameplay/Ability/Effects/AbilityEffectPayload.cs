using System.Collections.Generic;
using UnityEngine;

public readonly struct AbilityEffectPayload
{
	public readonly GameObject Caster;

	private readonly List<FeedbackEffect> _firstImpactFeedbacks;
	private readonly List<MechanicEffect> _everyImpactMechanics;
	private readonly List<FeedbackEffect> _everyImpactFeedbacks;

	public AbilityEffectPayload(
		GameObject caster,
		List<FeedbackEffect> firstImpact,
		List<MechanicEffect> everyImpactMechanic,
		List<FeedbackEffect> everyImpactFeedback)
	{
		Caster = caster;
		_firstImpactFeedbacks = firstImpact;
		_everyImpactMechanics = everyImpactMechanic;
		_everyImpactFeedbacks = everyImpactFeedback;
	}

	public void OnFirstImpact(GameObject source, GameObject firstTarget, Vector3 hitPosition)
	{
		if (_firstImpactFeedbacks == null) return;
		foreach (var effect in _firstImpactFeedbacks) effect.Execute(Caster, source, firstTarget, hitPosition);
	}

	public void OnImpact(GameObject source, GameObject target, Vector3 hitPosition)
	{
		if (_everyImpactMechanics != null)
		{
			foreach (var effect in _everyImpactMechanics)
			{
				if (effect.CanApplyTo(target))
				{
					effect.Execute(Caster, source, target, hitPosition);
				}
			}
		}

		if (_everyImpactFeedbacks != null)
			foreach (var effect in _everyImpactFeedbacks) effect.Execute(Caster, source, target, hitPosition);
	}
}
