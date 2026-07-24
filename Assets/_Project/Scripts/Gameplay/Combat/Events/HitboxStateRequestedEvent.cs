public readonly struct HitboxStateRequestedEvent : ILocalEvent
{
	public readonly bool IsActive;
	public readonly HitPayload Payload;

	public HitboxStateRequestedEvent(bool isActive, HitPayload payload)
	{
		IsActive = isActive;
		Payload = payload;
	}
}
