using System;
using UnityEngine;

[Serializable]
public struct LensDistortionEventPayload
{
	public float Intensity;
	public float Duration;
}

[CreateAssetMenu(fileName = "New Lens Distortion Channel", menuName = "Data/Events/PostProcess/Lens Distortion Channel")]
public class LensDistortionEventChannel : EventChannel<LensDistortionEventPayload> { }
