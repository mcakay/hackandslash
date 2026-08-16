public readonly struct HealthChangedEvent : ILocalEvent
{
	public readonly float CurrentHealth;
	public readonly float MaxHealth;

	public HealthChangedEvent(float currentHealth, float maxHealth)
	{
		CurrentHealth = currentHealth;
		MaxHealth = maxHealth;
	}
}
