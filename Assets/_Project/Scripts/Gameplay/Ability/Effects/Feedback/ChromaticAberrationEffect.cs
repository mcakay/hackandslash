using System;
using UnityEngine;

[Serializable]
public class ChromaticAberrationEffect : FeedbackEffect
{
	public ChromaticAberrationEventPayload Payload;

	[Header("Channel")]
	public ChromaticAberrationEventChannel Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		Channel.Raise(Payload);
	}
}
