using System;
using System.Collections.Generic;

public class StateMachine
{
	public IState CurrentState { get; private set; }

	private readonly Dictionary<Type, IState> _states = new();

	public void AddState(IState state)
	{
		Type type = state.GetType();
		if (!_states.ContainsKey(type))
		{
			_states.Add(type, state);
		}
	}

	public void ChangeState<T>() where T : class, IState
	{
		Type type = typeof(T);

		if (_states.TryGetValue(type, out IState nextState))
		{
			if (CurrentState == nextState)
			{
				return;
			}

			CurrentState?.OnExit();

			CurrentState = nextState;
			CurrentState?.OnEnter();
		}
	}

	public void Stop()
	{
		CurrentState?.OnExit();
		CurrentState = null;
	}

	public T GetState<T>() where T : class, IState
	{
		Type type = typeof(T);

		if (_states.TryGetValue(type, out IState state))
		{
			return state as T;
		}

		return null;
	}

	public void Tick(float deltaTime)
	{
		CurrentState?.OnUpdate(deltaTime);
	}
}
