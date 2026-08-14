using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
	public List<T> Items = new();

	public void Add(T thing)
	{
		if (!Items.Contains(thing))
		{
			Items.Add(thing);
		}
	}

	public void Remove(T thing)
	{
		Items.Remove(thing);

	}

	private void OnDisable()
    {
        Items.Clear();
    }
}
