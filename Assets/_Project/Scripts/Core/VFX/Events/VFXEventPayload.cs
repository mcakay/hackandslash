using System;
using UnityEngine;

[Serializable]
public struct VFXEventPayload
{
    public VFXFactorySO Factory;
    public Vector3 Position;
    public Quaternion Rotation;

	public VFXEventPayload(VFXFactorySO factory, Vector3 position, Quaternion rotation)
	{
		Factory = factory;
		Position = position;
		Rotation = rotation;
	}
}
