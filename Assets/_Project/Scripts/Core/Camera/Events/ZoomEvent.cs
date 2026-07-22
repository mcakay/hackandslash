using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ZoomPayload
{
	public float Amount;
	public float Duration;

	public ZoomPayload(float amount, float duration)
	{
		Amount = amount;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Zoom Channel", menuName = "Data/Events/Camera/Zoom Event")]
public class ZoomEventSO : BaseGameEventSO<ZoomPayload> { }
[System.Serializable]
public class ZoomUnityEvent : UnityEvent<ZoomPayload> { }
