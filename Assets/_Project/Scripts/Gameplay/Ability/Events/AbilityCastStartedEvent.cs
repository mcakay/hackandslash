public readonly struct AbilityCastStartedEvent : ILocalEvent
{
	public readonly float Speed;
	public readonly AbilitySO Ability;

	public AbilityCastStartedEvent(AbilitySO ability, float speed)
	{
		Ability = ability;
		Speed = speed;
	}
}
