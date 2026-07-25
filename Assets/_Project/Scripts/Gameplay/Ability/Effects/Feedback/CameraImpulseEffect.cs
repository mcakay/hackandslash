using System;
using UnityEngine;

[Serializable]
public class CameraImpulseEffect : FeedbackEffect
{
	public CameraImpulseEventPayload Payload;

	[Header("Channel")]
	public CameraImpulseEventSO Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
