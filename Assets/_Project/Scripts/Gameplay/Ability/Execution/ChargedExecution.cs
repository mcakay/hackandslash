using System;
using UnityEngine;

[Serializable]
public class ChargedExecution : AbilityExecution
{
    public override bool CanExecute => true;

    public float MaxChargeTime = 1.5f;
    private float _chargeStartTime;

	public override void OnInputStarted(AbilityRunner runner, AbilitySO context)
    {
        _chargeStartTime = Time.time;
        runner.StateMachine.ChangeState<WindupState>();
    }

    public override void OnInputEnded(AbilityRunner runner, AbilitySO context)
    {
        float holdDuration = Time.time - _chargeStartTime;
        float chargeMultiplier = Mathf.Clamp01(holdDuration / MaxChargeTime);

        runner.StateMachine.ChangeState<ExecutionState>();
    }
}
