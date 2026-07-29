using System;
using UnityEngine;

[Serializable]
public class ControllerHapticEffect : FeedbackEffect
{
	public ControllerHapticEventPayload Payload;

	[Header("Channel")]
	public ControllerHapticEventSO Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
