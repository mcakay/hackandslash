public readonly struct AbilityCastRequestedEvent : ILocalEvent
{
	public readonly int Id;

	public AbilityCastRequestedEvent(int id)
	{
		Id = id;
	}
}
