using UnityEngine;

public class TargetingController : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private LayerMask _groundLayer;

	private Camera _camera;
	private TargetingIndicator _activeIndicator;

	private TargetingSettings _settings;

	public Vector3 AimDirection { get; private set; }

	private void Awake()
	{
		_camera = Camera.main;
	}

	public void StartAiming(TargetingSettings settings)
	{
		_settings = settings;

		if (_settings.Factory != null)
		{
			_activeIndicator = _settings.Factory.Get(transform.position, Quaternion.identity);
		}
	}

	public void UpdateAim(Vector2 pointerPosition)
	{
		Ray ray = _camera.ScreenPointToRay(pointerPosition);

		if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _groundLayer))
		{
			Vector3 aimDirection = (hit.point - transform.position).normalized;
			aimDirection.y = 0;

			if (aimDirection != Vector3.zero)
			{
				AimDirection = aimDirection;

				if (_activeIndicator != null)
				{
					_activeIndicator.UpdateAim(transform.position, hit.point, _settings.Range, _settings.Size);
				}
			}
		}
	}

	public void StopAiming()
	{
		if (_activeIndicator != null)
		{
			_activeIndicator.ReturnToPool();
			_activeIndicator = null;
		}

		AimDirection = Vector3.zero;
	}
}
