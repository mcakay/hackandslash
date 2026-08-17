using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
	[SerializeField] private List<ScriptableObject> factories;

	private void Start()
	{
		Prewarm();
	}

	private void Prewarm()
	{
		if (factories == null || factories.Count == 0) return;

		foreach (var so in factories)
		{
			if (so is not IPoolFactory factory) return;

			factory.Prewarm();
		}
	}
}
