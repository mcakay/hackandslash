using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, TEvent, TResponse> : MonoBehaviour, IGameEventListener<T>
		where TEvent : BaseGameEventSO<T>
		where TResponse : UnityEvent<T>
{
	[SerializeField] private TEvent _gameEvent;

	[SerializeField] private TResponse _response;

	private void OnEnable()
	{
		if (_gameEvent)
		{
			_gameEvent.RegisterListener(this);
		}
	}

	private void OnDisable()
	{
		if (_gameEvent)
		{
			_gameEvent.UnregisterListener(this);
		}
	}

	public void OnEventRaised(T item)
	{
		_response?.Invoke(item);
	}
}
