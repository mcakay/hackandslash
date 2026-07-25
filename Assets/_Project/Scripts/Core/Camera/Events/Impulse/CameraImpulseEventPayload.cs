using System;

[Serializable]
public struct CameraImpulseEventPayload
{
	public float Intensity;
	public float Duration;

	public CameraImpulseEventPayload(float duration, float intensity)
	{
		Intensity = intensity;
		Duration = duration;
	}
}
