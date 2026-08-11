using System.Collections.Generic;

public readonly struct MovesetUpdatedEvent : ILocalEvent
{
	public readonly Dictionary<int, List<Ability>> Abilities;

	public MovesetUpdatedEvent(Dictionary<int, List<Ability>> abilities)
	{
		Abilities = abilities;
	}
}
