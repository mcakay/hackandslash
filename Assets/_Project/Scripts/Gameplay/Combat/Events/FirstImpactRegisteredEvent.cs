using System.Collections.Generic;
using UnityEngine;

public readonly struct FirstImpactRegisteredEvent : ILocalEvent
{
	public readonly List<SFXConfig> ImpactSFX;
	public readonly Vector3 Position;

	public FirstImpactRegisteredEvent(List<SFXConfig> impactSFX, Vector3 position)
	{
		ImpactSFX = impactSFX;
		Position = position;
	}
}
