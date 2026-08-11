using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LocalEventChannel))]
public class TargetingController : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private LayerMask _groundLayer;

	private Camera _camera;
	private TargetingIndicator _activeIndicator;
	private TargetingSettings _settings;
	private LocalEventChannel _channel;

	public Vector3 AimDirection { get; private set; }

	private readonly Dictionary<int, TargetingSettings> _targetedAbilities = new();

	private void Awake()
	{
		_camera = Camera.main;
		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<MovesetUpdatedEvent>(OnMovesetUpdated);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<MovesetUpdatedEvent>(OnMovesetUpdated);
	}

	private void OnMovesetUpdated(MovesetUpdatedEvent e)
	{
		_targetedAbilities.Clear();

		foreach (var kvp in e.Abilities)
		{
			int hashId = kvp.Key;
			List<Ability> abilityList = kvp.Value;

			if (abilityList != null && abilityList.Count > 0)
			{
				Ability firstAbility = abilityList[0];

				if (firstAbility.Data.IsTargeted)
				{
					_targetedAbilities[hashId] = firstAbility.Data.TargetingSettings;
				}
			}
		}
	}

	public bool TryGetTargetingSettings(int hash, out TargetingSettings settings)
	{
		return _targetedAbilities.TryGetValue(hash, out settings);
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
