using UnityEngine;

public class AbilitySO : ScriptableObject
{
	[Header("Identity")]
	public string Name;

	[Header("Animation")]
	public string AnimationTriggerName;
	public int AnimationHash => Animator.StringToHash(AnimationTriggerName);

	public AnimationClip Clip;

	[Min(0.1f)]
	public float AnimationSpeed = 1f;

	[Header("Timing")]
	public float ComboWindow = 1.0f;
	public float Duration => (Clip != null ? Clip.length : 0f) / AnimationSpeed;

	[Header("Phase Durations")]
	[Range(0f, 1f)] public float WindupPercentage = 0.2f;
	[Range(0f, 1f)] public float ExecutionPercentage = 0.3f;
	[Range(0f, 1f)] public float RecoveryPercentage = 0.5f;

	public float WindupDuration => Duration * WindupPercentage;
	public float ExecutionDuration => Duration * ExecutionPercentage;
	public float RecoveryDuration => Duration * RecoveryPercentage;
}
