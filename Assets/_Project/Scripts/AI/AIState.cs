using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI State", menuName = "Data/AI/State")]
public class AIState : ScriptableObject
{
	[SerializeReference] public List<IAction> actions = new();
	[SerializeReference] public List<AITransition> transitions = new();

	public void UpdateState(AIStateController controller)
	{
		DoActions(controller);
		CheckTransitions(controller);
	}

	private void DoActions(AIStateController controller)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			actions[i].Act(controller);
		}
	}

	private void CheckTransitions(AIStateController controller)
	{
		for (var i = 0; i < transitions.Count; i++)
		{
			if (transitions[i].decision == null) continue;

			bool decisionSucceeded = transitions[i].decision.Decide(controller);

			AIState nextState = decisionSucceeded ? transitions[i].trueState : transitions[i].falseState;

			if (nextState != null && nextState != this)
			{
				controller.TransitionToState(nextState);
				break;
			}
		}
	}
}
