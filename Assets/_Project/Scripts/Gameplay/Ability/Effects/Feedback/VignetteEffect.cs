using System;
using UnityEngine;

[Serializable]
public class VignetteEffect : FeedbackEffect
{
	public VignetteEventPayload Payload;

	[Header("Channel")]
	public VignetteEventChannel Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
