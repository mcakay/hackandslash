using System;

[Serializable]
public struct AITransition
{
    public AIDecision decision;
    public AIState trueState;
    public AIState falseState;
}
