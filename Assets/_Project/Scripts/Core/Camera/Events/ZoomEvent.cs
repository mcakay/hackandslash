using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ZoomPayload
{
	public float TargetFOV;
	public float Duration;

	public ZoomPayload(float targetFOV, float duration)
	{
		TargetFOV = targetFOV;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Zoom Channel", menuName = "Camera/Zoom Channel")]
public class ZoomEventSO : BaseGameEventSO<ZoomPayload> { }
[System.Serializable]
public class ZoomUnityEvent : UnityEvent<ZoomPayload> { }
public class ZoomEventListener : BaseGameEventListener<ZoomPayload, ZoomEventSO, ZoomUnityEvent> { }
