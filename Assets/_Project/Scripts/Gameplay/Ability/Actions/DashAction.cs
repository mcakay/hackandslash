using System;
using UnityEngine;

[Serializable]
public struct DashSettings
{
    [Header("Dash Physics")]
    public float Speed;
    public float Duration;

    [Header("Ghost Trail Visuals")]
    public GhostFactorySO Factory;
    public Material Material;

    [Range(1, 10)] public int Count;
    [Range(0.1f, 2f)] public float DistanceBetween;
    [Range(0f, 0.5f)] public float SpawnDelay;

    public float FadeDuration;
}

[Serializable]
public class DashAction : AbilityAction
{
    public DashSettings Settings;

    public override void Execute(GameObject caster, Ability ability)
    {
        if (caster == null) return;

        if (caster.TryGetComponent(out DashController dashController))
        {
            Vector3 dashDirection = caster.transform.forward;
            dashController.StartDash(dashDirection, in Settings);
        }
    }
}
