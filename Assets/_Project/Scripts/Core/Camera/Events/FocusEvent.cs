using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public struct FocusPayload
{
	public Transform Target;
	public float Duration;

	public FocusPayload(Transform target, float duration)
	{
		Target = target;
		Duration = duration;
	}
}

[CreateAssetMenu(fileName = "New Focus Channel", menuName = "Data/Events/Camera/Focus Event")]
public class FocusEventSO : BaseGameEventSO<FocusPayload> { }
[Serializable]
public class FocusUnityEvent : UnityEvent<FocusPayload> { }
