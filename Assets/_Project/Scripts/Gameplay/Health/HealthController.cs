using UnityEngine;

[RequireComponent(typeof(StatController))]
[RequireComponent(typeof(TagController))]
[RequireComponent(typeof(LocalEventChannel))]
public class HealthController : MonoBehaviour, IDamageable
{
	[SerializeField] private TagSO _deadTag;

	private LocalEventChannel _channel;

	private StatController _statController;
	private TagController _tagController;

	private float _maxHealth;
	private float _currentHealth;

	private void Awake()
	{
		_statController = GetComponent<StatController>();
		_tagController = GetComponent<TagController>();
		_channel = GetComponent<LocalEventChannel>();
	}

	private void Start()
	{
		_maxHealth = _statController.GetStat(StatType.MaxHealth);
		_currentHealth = _maxHealth;
	}

	public void TakeDamage(float damage)
	{
		_currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

		if (_currentHealth <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		if (_tagController != null && _deadTag != null)
		{
			_tagController.AddTag(_deadTag);
		}

		if (_channel != null)
		{
			_channel.Publish(new DeathEvent());
		}
	}
}
