using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Equipment/Weapon")]
public class WeaponSO : ScriptableObject
{
	public string Name;
	public GameObject Prefab;
}
