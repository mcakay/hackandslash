using System.Collections.Generic;
using UnityEngine;

public class Moveset
{
	public MovesetSO Data { get; private set; }

	public List<Ability> AllAbilities { get; private set; } = new();

	private readonly Dictionary<int, List<Ability>> _abilityLookup = new();

	private readonly AbilityController _controller;

	public Moveset(AbilityController controller, MovesetSO initialData)
	{
		_controller = controller;
		UpdateMoveset(initialData);
	}

	public void UpdateMoveset(MovesetSO data)
	{
		if (data == null || data == Data)
		{
			return;
		}

		Clear();
		Build(data);
		_controller.Channel.Publish(new MovesetUpdatedEvent(_abilityLookup));
	}

	public List<Ability> GetAbilities(int id)
	{
		if (_abilityLookup.TryGetValue(id, out var abilities))
		{
			return abilities;
		}
		return null;
	}

	public void Clear()
	{
		foreach (var ability in AllAbilities)
		{
			ability.Dispose();
		}
		_abilityLookup.Clear();
	}

	private void Build(MovesetSO data)
	{
		Data = data;
		Data.Initialize();

		foreach (var slot in Data.Slots)
		{
			int hash = Animator.StringToHash(slot.Name);
			List<Ability> abilityList = new();

			if (slot.Abilities != null)
			{
				foreach (var abilitySO in slot.Abilities)
				{
					var ability = new Ability(abilitySO);
					abilityList.Add(ability);
					AllAbilities.Add(ability);
				}

				_abilityLookup.Add(hash, abilityList);
			}
		}
	}
}
