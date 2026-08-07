using System;
using UnityEngine;

[Serializable]
public class LensDistortionEffect : FeedbackEffect
{
	public LensDistortionEventPayload Payload;

	[Header("Channel")]
	public LensDistortionEventChannel Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
