using System;
using UnityEngine;

[Serializable]
public class CameraImpulseEffect : FeedbackEffect
{
	public CameraImpulseEventPayload Payload;

	[Header("Channel")]
	public CameraImpulseEventChannel Channel;

	public override void Execute(GameObject caster, GameObject source, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
