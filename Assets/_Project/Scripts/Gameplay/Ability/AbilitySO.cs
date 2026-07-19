using UnityEngine;

public class AbilitySO : ScriptableObject
{
	[Header("Identity")]
	public string Name;

	[Header("Timing")]
	public float Duration;
	public float ComboWindow = 1.0f;

	[Header("Phase Percentages (0.0 - 1.0)")]
	[Range(0f, 1f)] public float WindupPercentage = 0.2f;
	[Range(0f, 1f)] public float ExecutionPercentage = 0.3f;
	[Range(0f, 1f)] public float RecoveryPercentage = 0.5f;
	[Range(0f, 1f)] public float CancelPercentage = 0.2f;

	public float WindupDuration
	{
		get
		{
			return Duration * WindupPercentage;
		}
	}

	public float ExecutionDuration
	{
		get
		{
			return Duration * ExecutionPercentage;
		}
	}

	public float RecoveryDuration
	{
		get
		{
			return Duration * RecoveryPercentage;
		}
	}

	public float EarlyCancelWindow
	{
		get
		{
			return RecoveryDuration * CancelPercentage;
		}
	}

	private void OnValidate()
	{
		float total = WindupPercentage + ExecutionPercentage + RecoveryPercentage;

		if (Mathf.Abs(total - 1.0f) > 0.01f)
		{
			Debug.LogWarning($"Total of phase percentages for ability '{Name}' is {total}, which does not equal 1.0. Please adjust the percentages to ensure they sum to 1.0.");
		}
	}
}
