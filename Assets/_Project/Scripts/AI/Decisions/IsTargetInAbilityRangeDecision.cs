using System;
using UnityEngine;


[Serializable]
public class IsTargetInAbilityRangeDecision : IDecision
{
    public bool Decide(AIStateController controller)
    {
        if (controller.TargetEntity == null || controller.SelectedAbility == null)
            return false;

        if (!controller.SelectedAbility.IsReady)
            return false;

        Vector3 offset = controller.TargetEntity.Transform.position - controller.transform.position;
        float sqrDistance = offset.sqrMagnitude;

        float realRange = controller.SelectedAbility.Data.Range + 0.1f;
        float sqrRange = realRange * realRange;

        return sqrDistance <= sqrRange;
    }
}
