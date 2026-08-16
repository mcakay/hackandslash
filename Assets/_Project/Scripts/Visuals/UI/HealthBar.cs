using UnityEngine;
using System;

[RequireComponent(typeof(Renderer))]
public class WorldSpaceHealthBar : MonoBehaviour
{
	[SerializeField] private LocalEventChannel _channel;

	private MaterialPropertyBlock _propBlock;
	private Renderer _renderer;
	private int _fillAmountHash;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();

		_propBlock = new MaterialPropertyBlock();
		_fillAmountHash = Shader.PropertyToID("_Fill_Amount");
	}

	private void OnEnable()
	{
		if (_channel != null)
		{
			_channel.Subscribe<HealthChangedEvent>(OnHealthChanged);
			_channel.Subscribe<DeathEvent>(OnDeath);
		}
	}

	private void OnDisable()
	{
		if (_channel != null)
		{
			_channel.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
			_channel.Unsubscribe<DeathEvent>(OnDeath);
		}
	}

	private void OnHealthChanged(HealthChangedEvent e)
	{
		float percentage = e.CurrentHealth / e.MaxHealth;

		_renderer.GetPropertyBlock(_propBlock);
		_propBlock.SetFloat(_fillAmountHash, percentage);
		_renderer.SetPropertyBlock(_propBlock);
	}

	private void OnDeath(DeathEvent e)
	{
		gameObject.SetActive(false);
	}
}
