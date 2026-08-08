using System;
using UnityEngine;

[Serializable]
public class KnockbackEffect : MechanicEffect
{
    public float Force = 15f;
    public float Duration = 0.2f;

    public bool FlattenY = true;

    public override void Execute(GameObject caster, GameObject source, GameObject target, Vector3 position)
    {
        if (target != null && target.TryGetComponent(out KnockbackReceiver receiver))
        {
            Vector3 direction = target.transform.position - caster.transform.position;

            if (FlattenY)
            {
                direction.y = 0f;
            }
            direction.Normalize();

            receiver.ApplyKnockback(direction, Force, Duration);
        }
    }
}
