using System.Collections.Generic;
using UnityEngine;

public class StatController : MonoBehaviour
{
	[SerializeField] private StatsSO _initialStats;

	private readonly Dictionary<StatType, float> _currentStats = new();

	private void Awake()
	{
		Initialize();
	}

	public float GetStat(StatType statType)
	{
		if (_currentStats.TryGetValue(statType, out var value))
		{
			return value;
		}

		return 0f;
	}

	private void Initialize()
	{
		if (_initialStats == null)
		{
			return;
		}

		foreach (var stat in _initialStats.InitialStats)
		{
			_currentStats[stat.Type] = stat.Value;
		}
	}
}
