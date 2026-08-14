using System;
using UnityEngine;

[Serializable]
public class UseAbilityAction : IAction
{
	public void Act(AIStateController controller)
	{
		if (controller.TargetEntity == null || controller.SelectedAbility == null) return;

		controller.NavMeshAgent.isStopped = true;

		int abilityHash = controller.SelectedAbility.Data.AnimationHash;
		controller.AbilityController.RequestCast(abilityHash);
	}
}
