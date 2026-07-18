using UnityEngine;

public class Loadout : MonoBehaviour
{
	[Header("Sockets")]
	[SerializeField] private Transform weaponSocket;

	[Header("Setup")]
	[SerializeField] private WeaponSO startingWeapon;

	private WeaponSO _currentWeaponData;
	private GameObject _currentWeaponInstance;

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
	}
}
