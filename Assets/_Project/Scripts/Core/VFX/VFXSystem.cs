using UnityEngine;

public class VFXSystem : MonoBehaviour
{
	public void OnVFXEvent(VFXEventPayload payload)
	{
		if (payload.Factory == null)
		{
			return;
		}

		payload.Factory.Get(payload.Position, payload.Rotation);
	}
}
