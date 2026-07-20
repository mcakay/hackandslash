public readonly struct MovesetUpdateRequestedEvent : ILocalEvent
{
	public readonly MovesetSO moveset;

	public MovesetUpdateRequestedEvent(MovesetSO moveset)
	{
		this.moveset = moveset;
	}
}
