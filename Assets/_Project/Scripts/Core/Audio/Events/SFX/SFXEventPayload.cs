using System;
using UnityEngine;

[Serializable]
public struct SFXEventPayload
{
	public AudioClip Clip;
	[Range(0f, 1f)] public float Volume;
	[Range(-3f, 3f)] public float Pitch;
	public Vector3 Position;

	public SFXEventPayload(AudioClip clip, float volume, float pitch, Vector3 position)
	{
		Clip = clip;
		Volume = volume;
		Pitch = pitch;
		Position = position;
	}
}
