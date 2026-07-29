public readonly struct HitboxRegisterRequestedEvent : ILocalEvent
{
	public readonly Hitbox hitbox;

	public HitboxRegisterRequestedEvent(Hitbox hitbox)
	{
		this.hitbox = hitbox;
	}
}
