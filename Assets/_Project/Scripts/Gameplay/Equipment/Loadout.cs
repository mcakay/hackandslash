using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class Loadout : MonoBehaviour
{
	[Header("Sockets")]
	[SerializeField] private Transform weaponSocket;

	[Header("Setup")]
	[SerializeField] private WeaponSO startingWeapon;

	private WeaponSO _currentWeaponData;
	private GameObject _currentWeaponInstance;

	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
	}

	private void Start()
	{
		if (startingWeapon != null)
		{
			Equip(startingWeapon);
		}
	}

	public void Equip(WeaponSO newWeapon)
	{
		if (newWeapon == null)
		{
			return;
		}

		if (_currentWeaponInstance != null)
		{
			Destroy(_currentWeaponInstance);
		}

		_currentWeaponData = newWeapon;

		if (_currentWeaponData.Prefab != null)
		{
			_currentWeaponInstance = Instantiate(_currentWeaponData.Prefab, weaponSocket);
			_currentWeaponInstance.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
		}
		var hitbox = _currentWeaponInstance.GetComponent<Hitbox>();

		_channel.Publish(new MovesetUpdateRequestedEvent(_currentWeaponData.Moveset));
		_channel.Publish(new HitboxUpdateRequestedEvent(hitbox));
	}
}
