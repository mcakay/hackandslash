using System;
using System.Collections.Generic;
using UnityEngine;

internal readonly struct BufferedInput
{
	public readonly int Id;
	public readonly float Timestamp;

	public BufferedInput(int id, float timestamp)
	{
		Id = id;
		Timestamp = timestamp;
	}
}

public class AbilityBuffer
{
	private readonly AbilityConfig _config;
	private readonly Queue<BufferedInput> _inputBuffer = new();

	private readonly Func<bool> _checkCanExecute;
	private readonly Action<int> _executeAbility;

	public AbilityBuffer(AbilityConfig config, Func<bool> checkCanExecute, Action<int> executeAbility)
	{
		_config = config;
		_checkCanExecute = checkCanExecute;
		_executeAbility = executeAbility;
	}

	public void Add(int id)
	{
		if (_config == null)
		{
			return;
		}

		if (_inputBuffer.Count < _config.MaxBufferSize)
		{
			_inputBuffer.Enqueue(new BufferedInput(id, Time.time));
		}
	}

	public void Process()
	{
		CleanExpiredInputs();
		TryExecuteNextAbility();
	}

	private void TryExecuteNextAbility()
	{
		if (_inputBuffer.Count > 0 && _checkCanExecute.Invoke())
		{
			int nextId = _inputBuffer.Dequeue().Id;
			_executeAbility.Invoke(nextId);
		}
	}

	private void CleanExpiredInputs()
	{
		if (_config == null)
		{
			return;
		}

		while (_inputBuffer.Count > 0)
		{
			float inputAge = Time.time - _inputBuffer.Peek().Timestamp;

			if (inputAge <= _config.BufferClearTime)
			{
				break;
			}

			_inputBuffer.Dequeue();
		}
	}
}
