using System;
using UnityEngine;

[Serializable]
public struct ControllerHapticEventPayload
{
    [Range(0f, 1f)] public float LowFrequency;
    [Range(0f, 1f)] public float HighFrequency;
    public float Duration;

    public ControllerHapticEventPayload(float lowFrequency, float highFrequency, float duration)
    {
        LowFrequency = lowFrequency;
        HighFrequency = highFrequency;
        Duration = duration;
    }
}
