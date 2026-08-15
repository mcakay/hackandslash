using System;

[Serializable]
public class AnimationVariantData
{
	public string ParameterName;
	public float[] PossibleValues;
	public float TransitionSpeed = 0.25f;
	public bool AutoChangeOverTime;
	public float MinTime = 3f;
	public float MaxTime = 8f;
}
