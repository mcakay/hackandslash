public readonly struct HitboxUpdateRequestedEvent : ILocalEvent
{
	public readonly Hitbox hitbox;

	public HitboxUpdateRequestedEvent(Hitbox hitbox)
	{
		this.hitbox = hitbox;
	}
}
