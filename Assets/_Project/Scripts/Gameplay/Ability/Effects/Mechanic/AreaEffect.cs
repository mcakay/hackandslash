using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AreaEffect : MechanicEffect
{
	[SerializeReference] public AbilityShape Shape;

	[SerializeReference] public List<MechanicEffect> Mechanics = new();
	[SerializeReference] public List<FeedbackEffect> Feedbacks = new();

	private static readonly Collider[] _hitResults = new Collider[30];
    private static readonly HashSet<Hurtbox> _hitHurtboxes = new();

	public override void Execute(GameObject caster, GameObject target, Vector3 hitPosition)
    {
        if (Shape == null) return;

        _hitHurtboxes.Clear();

        AbilityEffectPayload effectPayload = new(
            target,
            null,
            Mechanics,
            Feedbacks
        );

        int hitCount = Shape.GetTargets(hitPosition, caster.transform.forward, _hitResults);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hitCol = _hitResults[i];

            if (hitCol.gameObject == caster || hitCol.transform.root.gameObject == caster) continue;

            if (hitCol.TryGetComponent(out Hurtbox hurtbox))
            {
                if (_hitHurtboxes.Add(hurtbox))
                {
                    hurtbox.ReceiveHit(effectPayload, hitCol.transform.position);
                }
            }
        }
    }
}
