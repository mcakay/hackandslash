using System;
using UnityEngine;

[Serializable]
public struct VignetteEventPayload
{
	public float Intensity;
	public float Duration;
}

[CreateAssetMenu(fileName = "New Vignette Channel", menuName = "Data/Events/PostProcess/Vignette Channel")]
public class VignetteEventChannel : EventChannel<VignetteEventPayload> { }
