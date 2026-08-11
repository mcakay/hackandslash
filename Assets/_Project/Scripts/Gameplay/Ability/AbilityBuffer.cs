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
    private readonly AbilityConfigSO _config;
    private readonly AbilityController _controller;
    private readonly Queue<BufferedInput> _inputBuffer = new();

    public AbilityBuffer(AbilityConfigSO config, AbilityController controller)
    {
        _config = config;
        _controller = controller;
    }

    public void Add(int id)
    {
        if (_config == null) return;

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
        if (_inputBuffer.Count > 0 && _controller.CanExecuteNextAbility())
        {
            int nextId = _inputBuffer.Dequeue().Id;
            _controller.ExecuteAbility(nextId);
        }
    }

    private void CleanExpiredInputs()
    {
        if (_config == null) return;

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
