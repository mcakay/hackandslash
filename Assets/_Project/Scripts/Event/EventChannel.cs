using System;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
	private Action<T> _eventRaised;

	public void Raise(T payload) => _eventRaised?.Invoke(payload);
	public void Subscribe(Action<T> listener) => _eventRaised += listener;
	public void Unsubscribe(Action<T> listener) => _eventRaised -= listener;
}
