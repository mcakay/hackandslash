using System;
using System.Collections.Generic;
using UnityEngine;
using Alchemy.Inspector;


[Serializable]
public struct SFXConfig
{
	public List<AudioClip> Clips;

	[Range(0f, 1f)]
	public float Volume;

	[LabelText("Min Pitch")]
	[Range(0.1f, 3f)]
	public float MinPitch;

	[LabelText("Max Pitch")]
	[Range(0.1f, 3f)]
	public float MaxPitch;

	public readonly SFXEventPayload CreatePayload(Vector3 position)
	{
		var randomClip = Clips[UnityEngine.Random.Range(0, Clips.Count)];
		return new SFXEventPayload(randomClip, Volume, UnityEngine.Random.Range(MinPitch, MaxPitch), position);
	}

	public readonly bool IsValid()
	{
		return Clips != null && Clips.Count > 0;
	}
}

