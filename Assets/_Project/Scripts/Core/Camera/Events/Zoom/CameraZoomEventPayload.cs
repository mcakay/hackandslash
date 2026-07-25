using System;

[Serializable]
public struct CameraZoomEventPayload
{
	public float Amount;
	public float Duration;

	public CameraZoomEventPayload(float duration, float amount)
	{
		Amount = amount;
		Duration = duration;
	}
}
