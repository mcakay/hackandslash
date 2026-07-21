public readonly struct HitboxStateRequestedEvent : ILocalEvent
{
	public readonly bool IsActive;

	public HitboxStateRequestedEvent(bool isActive)
	{
		IsActive = isActive;
	}
}
