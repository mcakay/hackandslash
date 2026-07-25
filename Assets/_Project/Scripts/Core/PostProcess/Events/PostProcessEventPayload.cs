using System;
using UnityEngine;

[Serializable]
public struct PostProcessEventPayload
{
	[Header("Chromatic Aberration")]
	public bool UseChromaticAberration;
	public float CAIntensity;
	public float CADuration;

	[Header("Lens Distortion")]
	public bool UseLensDistortion;
	public float LDIntensity;
	public float LDDuration;

	[Header("Vignette")]
	public bool UseVignette;
	public float VigIntensity;
	public float VigDuration;
}
