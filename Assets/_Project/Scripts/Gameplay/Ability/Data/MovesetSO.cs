using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Moveset", menuName = "Data/Moveset")]
public class MovesetSO : ScriptableObject
{
	[Serializable]
	public struct AbilitySlot
	{
		public string Name;
		public List<AbilitySO> Abilities;
	}

	public List<AbilitySlot> Slots = new();

	private Dictionary<int, List<AbilitySO>> _lookup;

	public void Initialize()
	{
		if (_lookup != null)
		{
			return;
		}

		_lookup = new Dictionary<int, List<AbilitySO>>();

		foreach (var slot in Slots)
		{
			int id = Animator.StringToHash(slot.Name);

			if (!_lookup.ContainsKey(id))
			{
				_lookup.Add(id, slot.Abilities);
			}
		}
	}

	public List<AbilitySO> GetAbilities(int id)
	{
		if (_lookup.TryGetValue(id, out List<AbilitySO> abilities))
		{
			return abilities;
		}
		return null;
	}
}
