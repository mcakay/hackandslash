using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct MusicPayload
{
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
    public bool Loop;
}

[CreateAssetMenu(fileName = "New Music Channel", menuName = "Data/Events/Audio/Music Event")]
public class MusicEventSO : BaseGameEventSO<MusicPayload> { }
[System.Serializable] public class MusicUnityEvent : UnityEvent<MusicPayload> { }
public class MusicEventListener : BaseGameEventListener<MusicPayload, MusicEventSO, MusicUnityEvent> { }
