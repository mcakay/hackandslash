using System;

[Serializable]
public class InstantExecution : AbilityExecution
{
    public override bool CanExecute => true;

    public override void OnInputStarted(AbilityRunner runner, AbilitySO context)
    {
		runner.StateMachine.ChangeState<WindupState>();
    }

    public override void OnInputEnded(AbilityRunner runner, AbilitySO context)
    {
		runner.StateMachine.ChangeState<ExecutionState>();
    }
}
