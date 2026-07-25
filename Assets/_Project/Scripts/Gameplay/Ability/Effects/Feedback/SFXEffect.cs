using System;
using UnityEngine;

[Serializable]
public class SFXEffect : FeedbackEffect
{
	[Header("Settings")]
	public AudioClip Clip;

	[Range(0f, 1f)]
	public float Volume = 1f;

	[Range(-3f, 3f)]
	public float Pitch = 1f;

	public bool RandomizePitch = true;

	[Header("Channel")]
	public SFXEventSO Channel;

	public override void Execute(GameObject caster, GameObject target, Vector3 position)
	{
		if (Clip == null || Channel == null) return;

		float finalPitch = RandomizePitch
			? Pitch * UnityEngine.Random.Range(0.9f, 1.1f)
			: Pitch;

		SFXEventPayload payload = new(Clip, Volume, finalPitch, position);

		Channel.Raise(payload);
	}
}
