using System;
using UnityEngine;

[Serializable]
public struct CameraZoomEventPayload
{
	public float Amount;
	public float Duration;

	public CameraZoomEventPayload(float duration, float amount)
	{
		Amount = amount;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Camera Zoom Channel", menuName = "Data/Events/Camera/Camera Zoom Event")]
public class CameraZoomEventChannel : EventChannel<CameraZoomEventPayload> { }
