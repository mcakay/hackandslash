public readonly struct WeaponEquippedEvent : ILocalEvent
{
	public readonly WeaponSO Data;
	public readonly Hitbox Hitbox;

	public WeaponEquippedEvent(WeaponSO data, Hitbox hitbox)
	{
		Data = data;
		Hitbox = hitbox;
	}
}
