using System;
using UnityEngine;

[Serializable]
public class TimeScaleEffect : FeedbackEffect
{
	public TimeScaleEventPayload Payload;

	[Header("Channel")]
	public TimeScaleEventSO Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
