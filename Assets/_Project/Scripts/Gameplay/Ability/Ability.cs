using System;
using UnityEngine;

public class Ability : IDisposable
{
	public AbilitySO Data { get; private set; }

	public bool IsReady => !_cooldownTimer.IsRunning;
	public float CooldownRemaining => _cooldownTimer.IsRunning ? Data.Cooldown - _cooldownTimer.ElapsedTime : 0f;

	private readonly Timer _cooldownTimer;

	public Ability(AbilitySO data)
	{
		Data = data;
		_cooldownTimer = new Timer();
	}

	public void Tick(float deltaTime)
	{
		_cooldownTimer.Tick(deltaTime);
	}

	public void StartCooldown()
	{
		if (Data.Cooldown > 0f)
		{
			_cooldownTimer.Start(Data.Cooldown);
		}
	}

	public void Dispose()
	{

	}

	public AbilityEffectPayload CreateEffectPayload(GameObject caster)
	{
		return new AbilityEffectPayload(
			caster,
			Data.FirstImpactFeedbacks,
			Data.EveryImpactMechanics,
			Data.EveryImpactFeedbacks
		);
	}

	public void StartExecute(GameObject caster)
	{
		foreach (var action in Data.StartActions)
		{
			action.Execute(caster, this);
		}

		foreach (var feedback in Data.StartFeedbacks)
		{
			feedback.Execute(caster, caster, caster, caster.transform.position);
		}
	}

	public void EndExecute(GameObject caster)
	{
		foreach (var action in Data.EndActions)
		{
			action.Execute(caster, this);
		}

		foreach (var feedback in Data.EndFeedbacks)
		{
			feedback.Execute(caster, caster, caster, caster.transform.position);
		}
	}
}
