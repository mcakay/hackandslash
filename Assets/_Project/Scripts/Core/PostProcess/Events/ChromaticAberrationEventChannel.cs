using System;
using UnityEngine;

[Serializable]
public struct ChromaticAberrationEventPayload
{
	public float Intensity;
	public float Duration;
}

[CreateAssetMenu(fileName = "New Chromatic Aberration Channel", menuName = "Data/Events/PostProcess/Chromatic Aberration Channel")]
public class ChromaticAberrationEventChannel : EventChannel<ChromaticAberrationEventPayload> { }
