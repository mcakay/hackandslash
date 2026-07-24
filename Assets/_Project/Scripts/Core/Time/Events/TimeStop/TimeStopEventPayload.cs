using System;

[Serializable]
public struct TimeStopEventPayload
{
    public float Duration;
    public float TimeScale;

    public TimeStopEventPayload(float duration, float timeScale = 0f)
    {
        Duration = duration;
        TimeScale = timeScale;
    }
}
