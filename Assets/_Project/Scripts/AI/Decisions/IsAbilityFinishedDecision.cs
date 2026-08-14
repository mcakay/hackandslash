using System;

[Serializable]
public class IsAbilityFinishedDecision : IDecision
{
	public bool Decide(AIStateController controller)
	{
		return controller.AbilityController.StateMachine.CurrentState == null;
	}
}
