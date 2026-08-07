using UnityEngine;

public class VFXSystem : MonoBehaviour
{
	[SerializeField] private VFXEventChannel _vfxEventChannel;

	private void OnEnable()
	{
		_vfxEventChannel.Subscribe(OnVFXEvent);
	}

	private void OnDisable()
	{
		_vfxEventChannel.Unsubscribe(OnVFXEvent);
	}

	public void OnVFXEvent(VFXEventPayload payload)
	{
		if (payload.Factory == null)
		{
			return;
		}

		payload.Factory.Get(payload.Position, payload.Rotation);
	}
}
