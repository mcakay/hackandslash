using System;

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
