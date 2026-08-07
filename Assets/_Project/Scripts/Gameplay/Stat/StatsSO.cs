using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Data/Stats")]
public class StatsSO : ScriptableObject
{
	public List<StatSetup> InitialStats;
}
