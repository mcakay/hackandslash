using System;
using UnityEngine;

[Serializable]
public class PostProcessEffect : FeedbackEffect
{
	public PostProcessEventPayload Payload;

	[Header("Channel")]
	public PostProcessEventSO Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 hitPosition)
	{
		Channel.Raise(Payload);
	}
}
