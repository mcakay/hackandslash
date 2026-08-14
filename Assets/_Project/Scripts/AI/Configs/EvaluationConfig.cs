using UnityEngine;

[CreateAssetMenu(fileName = "Evaluation Config", menuName = "Data/Configs/Evaluation Config")]
public class EvaluationConfig : ScriptableObject
{
	public float ThinkInterval = 0.5f;
	public float IntervalVariance = 0.1f;
	public float MaxConsiderationSqrDistance = 2500f;
	public float StickinessBonus = 20f;
	public float ScoreNoise = 5f;
}
