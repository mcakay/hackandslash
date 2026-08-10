using System.Collections.Generic;

public readonly struct TargetingDataUpdatedEvent : ILocalEvent
{
	public readonly Dictionary<int, TargetingSettings> TargetedAbilities;

	public TargetingDataUpdatedEvent(Dictionary<int, TargetingSettings> targetedAbilities)
	{
		TargetedAbilities = targetedAbilities;
	}
}
