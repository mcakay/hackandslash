using UnityEngine;

public class AbilitySO : ScriptableObject
{
	[Header("Identity")]
	public string Name;

	[Header("Animation")]
	public string AnimationTriggerName;
	public int AnimationHash => Animator.StringToHash(AnimationTriggerName);

	[Header("Timing")]
	public float Duration;
	public float ComboWindow = 1.0f;

	[Header("Phase Durations")]
	[Range(0f, 1f)] public float WindupDuration = 0.2f;
	[Range(0f, 1f)] public float ExecutionDuration = 0.3f;
	[Range(0f, 1f)] public float RecoveryDuration = 0.5f;
	[Range(0f, 1f)] public float CancelDuration = 0.2f;
}
