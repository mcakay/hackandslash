using System;
using UnityEngine;

[Serializable]
public class VFXEffect : FeedbackEffect
{
	[Header("Settings")]
	public VFXFactorySO Factory;

	[Header("Channel")]
	public VFXEventChannel Channel;

	public override void Execute(GameObject caster, GameObject source, GameObject target, Vector3 position)
	{
		Channel.Raise(new VFXEventPayload(Factory, target.transform.position, target.transform.rotation));
	}
}
