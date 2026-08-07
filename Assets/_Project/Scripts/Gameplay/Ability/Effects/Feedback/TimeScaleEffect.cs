using System;
using UnityEngine;

[Serializable]
public class TimeScaleEffect : FeedbackEffect
{
	public TimeScaleEventPayload Payload;

	[Header("Channel")]
	public TimeScaleEventChannel Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
