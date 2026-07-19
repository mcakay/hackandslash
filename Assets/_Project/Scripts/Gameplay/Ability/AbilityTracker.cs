using System.Collections.Generic;
using UnityEngine;

public class AbilityTracker
{
	public AbilitySO CurrentAbility { get; private set; }

	private int _currentAbilityId;
	private int _currentAbilityIndex;
	private float _lastAbilityTime;

	public void Advance(int id, MovesetSO moveset)
	{
		if (moveset == null)
		{
			return;
		}

		List<AbilitySO> abilities = moveset.GetAbilities(id);

		if (abilities == null || abilities.Count == 0)
		{
			return;
		}

		if (_currentAbilityId != id)
		{
			Reset();
			_currentAbilityId = id;
		}

		CurrentAbility = abilities[_currentAbilityIndex];
		_lastAbilityTime = Time.time;

		_currentAbilityIndex++;
		if (_currentAbilityIndex >= abilities.Count)
		{
			_currentAbilityIndex = 0;
		}
	}

	public void Tick(bool isExecuting)
	{
		if (_currentAbilityIndex == 0 || CurrentAbility == null)
		{
			return;
		}

		if (isExecuting)
		{
			return;
		}

		if (Time.time - _lastAbilityTime > CurrentAbility.ComboWindow)
		{
			Reset();
		}
	}

	public void Reset()
	{
		_currentAbilityIndex = 0;
		_currentAbilityId = 0;
		CurrentAbility = null;
	}
}
