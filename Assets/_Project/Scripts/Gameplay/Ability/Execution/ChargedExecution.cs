using System;
using UnityEngine;

[Serializable]
public class ChargedExecution : AbilityExecution
{
    public override bool CanExecute => true;

    public float MaxChargeTime = 1.5f;
    private float _chargeStartTime;

	public override void OnInputStarted(AbilityController controller, Ability ability)
    {
        _chargeStartTime = Time.time;
        controller.StateMachine.ChangeState<WindupState>();
    }

    public override void OnInputEnded(AbilityController controller, Ability ability)
    {
        float holdDuration = Time.time - _chargeStartTime;
        float chargeMultiplier = Mathf.Clamp01(holdDuration / MaxChargeTime);

        controller.StateMachine.ChangeState<ExecutionState>();
    }
}
