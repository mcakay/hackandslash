using System;
using UnityEngine;

[Serializable]
public struct TimeScaleEventPayload
{
    public float Duration;
    public float TimeScale;

    public TimeScaleEventPayload(float duration, float timeScale = 0f)
    {
        Duration = duration;
        TimeScale = timeScale;
    }
}

[CreateAssetMenu(fileName = "New Time Scale Channel", menuName = "Data/Events/Time/Time Scale Channel")]
public class TimeScaleEventChannel : EventChannel<TimeScaleEventPayload> { }


