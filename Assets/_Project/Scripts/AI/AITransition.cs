using System;
using UnityEngine;

[Serializable]
public class AITransition
{
	[SerializeReference] public IDecision decision;
	public AIState trueState;
	public AIState falseState;
}
