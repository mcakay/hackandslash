using System;
using UnityEngine;

[Serializable]
public struct CameraImpulseEventPayload
{
	public float Intensity;
	public float Duration;

	public CameraImpulseEventPayload(float duration, float intensity)
	{
		Intensity = intensity;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Camera Impulse Channel", menuName = "Data/Events/Camera/Camera Impulse Event")]
public class CameraImpulseEventChannel : EventChannel<CameraImpulseEventPayload> { }
