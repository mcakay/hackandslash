using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct ShakePayload
{
	public float Intensity;
	public float Duration;

	public ShakePayload(float intensity, float duration)
	{
		Intensity = intensity;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Shake Channel", menuName = "Data/Events/Camera/Shake Event")]
public class ShakeEventSO : BaseGameEventSO<ShakePayload> { }
[Serializable]
public class ShakeUnityEvent : UnityEvent<ShakePayload> { }
