public readonly struct AbilityCastStartedEvent : ILocalEvent
{
	public readonly float Speed;
	public readonly Ability Ability;

	public AbilityCastStartedEvent(Ability ability, float speed)
	{
		Ability = ability;
		Speed = speed;
	}
}
