public readonly struct AbilityCastStartedEvent : ILocalEvent
{
	public readonly int Hash;
	public readonly float Speed;

	public AbilityCastStartedEvent(int hash, float speed)
	{
		Hash = hash;
		Speed = speed;
	}
}
