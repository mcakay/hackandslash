using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;

public abstract class AbilitySO : ScriptableObject
{
    [FoldoutGroup("Identity")]
    public string Name;

    [FoldoutGroup("Animation")]
    public string AnimationTriggerName;
    public int AnimationHash => Animator.StringToHash(AnimationTriggerName);

    [FoldoutGroup("Animation")]
    public AnimationClip Clip;

    [FoldoutGroup("Animation")]
    [Min(0.1f)]
    public float AnimationSpeed = 1f;

    [FoldoutGroup("Timing")]
    public float ComboWindow = 1.0f;
    public float Duration => (Clip != null ? Clip.length : 0f) / AnimationSpeed;

    [FoldoutGroup("Phase Durations")]
    [Range(0f, 1f)] public float WindupPercentage = 0.2f;

    [FoldoutGroup("Phase Durations")]
    [Range(0f, 1f)] public float ExecutionPercentage = 0.3f;

    [FoldoutGroup("Phase Durations")]
    [Range(0f, 1f)] public float RecoveryPercentage = 0.5f;

    [FoldoutGroup("Combat Effects")]
    [OnValueChanged(nameof(ResetTimeStopValues))]
    public bool IsTimeStop;

    [FoldoutGroup("Combat Effects")]
    [ShowIf(nameof(IsTimeStop))] [Min(0f)]
    public float TimeStopDuration;

	[FoldoutGroup("Combat Effects")]
	[ShowIf(nameof(IsTimeStop))] [Min(0f)]
	public float TimeStopScale = 0f;

    [FoldoutGroup("Combat Effects")]
    [OnValueChanged(nameof(ResetFovValues))]
    public bool IsFovZoom;

    [FoldoutGroup("Combat Effects")]
    [ShowIf(nameof(IsFovZoom))]
    public float FovZoomAmount;

    [FoldoutGroup("Combat Effects")]
    [ShowIf(nameof(IsFovZoom))] [Min(0f)]
    public float FovZoomDuration;

    [FoldoutGroup("Combat Effects")]
    [OnValueChanged(nameof(ResetShakeValues))]
    public bool IsScreenShake;

    [FoldoutGroup("Combat Effects")]
    [ShowIf(nameof(IsScreenShake))]
    public float ScreenShakeIntensity;

    [FoldoutGroup("Combat Effects")]
    [ShowIf(nameof(IsScreenShake))] [Min(0f)]
    public float ScreenShakeDuration;

    [FoldoutGroup("Physics")]
    public float KnockbackForce = 0f;

    [FoldoutGroup("Audio")]
    [LabelText("Cast SFX Layers")]
    public List<SFXConfig> CastSFX;

    [FoldoutGroup("Audio")]
    [LabelText("Impact SFX Layers")]
    public List<SFXConfig> ImpactSFX;

    public float WindupDuration => Duration * WindupPercentage;
    public float ExecutionDuration => Duration * ExecutionPercentage;
    public float RecoveryDuration => Duration * RecoveryPercentage;

    public abstract void StartExecute(GameObject caster, LocalEventChannel channel);
    public abstract void EndExecute(GameObject caster, LocalEventChannel channel);

    private void ResetTimeStopValues() { if (!IsTimeStop) TimeStopDuration = 0f; }
    private void ResetFovValues() { if (!IsFovZoom) { FovZoomAmount = 0f; FovZoomDuration = 0f; } }
    private void ResetShakeValues() { if (!IsScreenShake) { ScreenShakeIntensity = 0f; ScreenShakeDuration = 0f; } }
}
