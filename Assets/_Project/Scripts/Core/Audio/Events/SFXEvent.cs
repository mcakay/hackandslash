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
}

[CreateAssetMenu(fileName = "New SFX Channel", menuName = "Audio/SFX Channel")]
public class SFXEventSO : BaseGameEventSO<SFXPayload> { }
[Serializable] public class SFXUnityEvent : UnityEvent<SFXPayload> { }
public class SFXEventListener : BaseGameEventListener<SFXPayload, SFXEventSO, SFXUnityEvent> { }
