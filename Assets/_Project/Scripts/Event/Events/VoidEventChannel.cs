using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/Primitive/Void Event Channel")]
public class VoidEventChannel : ScriptableObject
{
	private Action _eventRaised;

	public void Raise() => _eventRaised?.Invoke();
	public void Subscribe(Action listener) => _eventRaised += listener;
	public void Unsubscribe(Action listener) => _eventRaised -= listener;
}
