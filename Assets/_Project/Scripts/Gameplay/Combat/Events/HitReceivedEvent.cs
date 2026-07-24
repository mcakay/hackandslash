using UnityEngine;

public readonly struct HitReceivedEvent : ILocalEvent
{
    public readonly HitPayload Payload;
    public readonly Vector3 SourcePosition;

    public HitReceivedEvent(HitPayload payload, Vector3 sourcePosition)
    {
        Payload = payload;
        SourcePosition = sourcePosition;
    }
}
