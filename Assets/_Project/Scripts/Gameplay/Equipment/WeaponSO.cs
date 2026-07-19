using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Data/Equipments/Weapon")]
public class WeaponSO : ScriptableObject
{
	public string Name;
	public GameObject Prefab;

	public MovesetSO Moveset;
}
