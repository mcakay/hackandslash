using UnityEngine;
using Alchemy.Inspector;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Ability", menuName = "Data/Ability")]
public class AbilitySO : ScriptableObject
{
	[FoldoutGroup("Identity")]
	public string Name;
	public Sprite Icon;
	public float Range = 1f;

	[FoldoutGroup("Animation")]
	public string AnimationTriggerName;
	public int AnimationHash => Animator.StringToHash(AnimationTriggerName);

	[FoldoutGroup("Animation")]
	public AnimationClip Clip;

	[FoldoutGroup("Animation")]
	[Min(0.1f)]
	public float AnimationSpeed = 1f;

	[FoldoutGroup("Targeting")]
	public bool IsTargeted;

	[FoldoutGroup("Targeting")]
	[ShowIf(nameof(IsTargeted))]
	public TargetingSettings TargetingSettings;

	[FoldoutGroup("Timing")]
	public float Cooldown = 0f;
	public float ComboWindow = 1.0f;
	public float Duration => (Clip != null ? Clip.length : 0f) / AnimationSpeed;

	[FoldoutGroup("Phase Durations")]
	[Range(0f, 1f)] public float WindupPercentage = 0.2f;

	[FoldoutGroup("Phase Durations")]
	[Range(0f, 1f)] public float ExecutionPercentage = 0.3f;

	[FoldoutGroup("Phase Durations")]
	[Range(0f, 1f)] public float RecoveryPercentage = 0.5f;

	[FoldoutGroup("1. Start Phase")]
	[LabelText("Start Actions")]
	[SerializeReference] public List<AbilityAction> StartActions = new();

	[FoldoutGroup("1. Start Phase")]
	[LabelText("Start Feedbacks")]
	[SerializeReference] public List<FeedbackEffect> StartFeedbacks = new();

	[FoldoutGroup("2. Impact Phase")]
	[LabelText("Spawns Payload")]
	public bool HasImpact;

	[FoldoutGroup("2. Impact Phase")]
	[ShowIf(nameof(HasImpact))]
	[LabelText("First Impact Feedbacks)")]
	[SerializeReference] public List<FeedbackEffect> FirstImpactFeedbacks = new();

	[FoldoutGroup("2. Impact Phase")]
	[ShowIf(nameof(HasImpact))]
	[LabelText("Every Impact Mechanics")]
	[SerializeReference] public List<MechanicEffect> EveryImpactMechanics = new();

	[FoldoutGroup("2. Impact Phase")]
	[ShowIf(nameof(HasImpact))]
	[LabelText("Every Impact Feedbacks")]
	[SerializeReference] public List<FeedbackEffect> EveryImpactFeedbacks = new();

	[FoldoutGroup("3. End Phase")]
	[LabelText("End Actions")]
	[SerializeReference] public List<AbilityAction> EndActions = new();

	[FoldoutGroup("3. End Phase")]
	[LabelText("End Feedbacks")]
	[SerializeReference] public List<FeedbackEffect> EndFeedbacks = new();

	[FoldoutGroup("Execution")]
	[LabelText("Execution Strategy")]
	[SerializeReference] public AbilityExecution Execution;

	[FoldoutGroup("AI Evaluation")]
	[LabelText("AI Evaluation Settings")]
	public bool IsEvaluatedByAI = true;

	[FoldoutGroup("AI Evaluation")]
	[ShowIf(nameof(IsEvaluatedByAI))]
	[LabelText("Utility Score")]
	public float UtilityScore = 1f;

	[FoldoutGroup("AI Evaluation")]
	[ShowIf(nameof(IsEvaluatedByAI))]
	[SerializeReference] public List<IEvaluator> Evaluators = new();

	public float WindupDuration => Duration * WindupPercentage;
	public float ExecutionDuration => Duration * ExecutionPercentage;
	public float RecoveryDuration => Duration * RecoveryPercentage;
}
