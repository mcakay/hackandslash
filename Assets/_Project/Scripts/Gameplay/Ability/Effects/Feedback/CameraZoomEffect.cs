using System;
using UnityEngine;

[Serializable]
public class CameraZoomEffect : FeedbackEffect
{
	public CameraZoomEventPayload Payload;

	[Header("Channel")]
	public CameraZoomEventChannel Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
