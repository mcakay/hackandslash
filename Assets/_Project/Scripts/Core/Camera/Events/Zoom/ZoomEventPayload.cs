using System;

[Serializable]
public struct ZoomEventPayload
{
	public float Amount;
	public float Duration;

	public ZoomEventPayload(float amount, float duration)
	{
		Amount = amount;
		Duration = duration;
	}
}
