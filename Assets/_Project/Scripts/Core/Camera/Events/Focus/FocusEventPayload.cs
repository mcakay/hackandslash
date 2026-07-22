using System;
using UnityEngine;

[Serializable]
public struct FocusEventPayload
{
	public Transform Target;
	public float Duration;

	public FocusEventPayload(Transform target, float duration)
	{
		Target = target;
		Duration = duration;
	}
}
