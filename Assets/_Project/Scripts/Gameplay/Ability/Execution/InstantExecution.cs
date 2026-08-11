using System;

[Serializable]
public class InstantExecution : AbilityExecution
{
    public override bool CanExecute => true;

    public override void OnInputStarted(AbilityController controller, Ability ability)
    {
		controller.StateMachine.ChangeState<WindupState>();
    }

    public override void OnInputEnded(AbilityController controller, Ability ability)
    {
		controller.StateMachine.ChangeState<ExecutionState>();
    }
}
