using System;
using UnityEngine;

[Serializable]
public class SFXEffect : FeedbackEffect
{
    [Header("Settings")]
    public AudioClip[] Clips;

    [Range(0f, 1f)]
    public float Volume = 1f;

    [Range(-3f, 3f)]
    public float Pitch = 1f;

    public bool RandomizePitch = true;

    [Header("Channel")]
    public SFXEventChannel Channel;

    public override void Execute(GameObject caster, GameObject source, GameObject target, Vector3 position)
    {
        if (Clips == null || Clips.Length == 0 || Channel == null) return;

        int randomIndex = UnityEngine.Random.Range(0, Clips.Length);
        AudioClip selectedClip = Clips[randomIndex];

        if (selectedClip == null) return;

        float finalPitch = RandomizePitch
            ? Pitch * UnityEngine.Random.Range(0.9f, 1.1f)
            : Pitch;

        SFXEventPayload payload = new(selectedClip, Volume, finalPitch, position);

        Channel.Raise(payload);
    }
}
