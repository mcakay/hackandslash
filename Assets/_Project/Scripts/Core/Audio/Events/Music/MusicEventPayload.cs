using System;
using UnityEngine;

[Serializable]
public struct MusicEventPayload
{
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
    public bool Loop;
}
