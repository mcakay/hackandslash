using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct SFXPayload
{
	public AudioClip Clip;
	[Range(0f, 1f)] public float Volume;
	[Range(-3f, 3f)] public float Pitch;
	public Vector3 Position;

	public SFXPayload(AudioClip clip, float volume, float pitch, Vector3 position)
	{
		Clip = clip;
		Volume = volume;
		Pitch = pitch;
		Position = position;
	}
}

[CreateAssetMenu(fileName = "New SFX Channel", menuName = "Data/Events/Audio/SFX Event")]
public class SFXEventSO : BaseGameEventSO<SFXPayload> { }
[Serializable] public class SFXUnityEvent : UnityEvent<SFXPayload> { }
