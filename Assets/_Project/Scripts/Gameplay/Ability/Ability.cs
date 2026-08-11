using System;

public class Ability : IDisposable
{
	public AbilitySO Data { get; private set; }

	public bool IsReady => !_cooldownTimer.IsRunning;

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
		_cooldownTimer.Start(Data.Cooldown);
	}

	public void Dispose()
	{

	}
}
