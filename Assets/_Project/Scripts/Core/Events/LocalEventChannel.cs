using System;
using System.Collections.Generic;
using UnityEngine;

public class LocalEventChannel : MonoBehaviour
{
	private readonly Dictionary<Type, Delegate> _subscribers = new();

	public void Subscribe<T>(Action<T> callback) where T : struct, ILocalEvent
	{
		Type type = typeof(T);

		if (!_subscribers.ContainsKey(type))
		{
			_subscribers[type] = null;
		}

		_subscribers[type] = Delegate.Combine(_subscribers[type], callback);
	}

	public void Unsubscribe<T>(Action<T> callback) where T : struct, ILocalEvent
	{
		Type type = typeof(T);

		if (_subscribers.ContainsKey(type))
		{
			_subscribers[type] = Delegate.Remove(_subscribers[type], callback);
		}
	}

	public void Publish<T>(T payload) where T : struct, ILocalEvent
	{
		Type type = typeof(T);

		if (_subscribers.TryGetValue(type, out Delegate callback))
		{
			(callback as Action<T>)?.Invoke(payload);
		}
	}
}
