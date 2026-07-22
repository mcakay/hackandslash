using System;

[Serializable]
public struct ShakeEventPayload
{
	public float Intensity;
	public float Duration;

	public ShakeEventPayload(float intensity, float duration)
	{
		Intensity = intensity;
		Duration = duration;
	}
}
