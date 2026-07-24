public readonly struct AbilityExecutionStartedEvent : ILocalEvent
{
	public readonly AbilitySO Ability;

	public AbilityExecutionStartedEvent(AbilitySO ability)
	{
		Ability = ability;
	}
}
