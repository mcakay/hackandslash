using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class HUDHealth : MonoBehaviour
{
	[SerializeField] LocalEventChannel channel;

	private UIDocument _document;

	private VisualElement _healthBarFill;
	private Label _healthText;

	private void Awake()
	{
		_document = GetComponent<UIDocument>();
		var root = _document.rootVisualElement;
		_healthBarFill = root.Q<VisualElement>("Health-Bar-Fill");
		_healthText = root.Q<Label>("Health-Text");
	}

	private void OnEnable()
	{
		if (channel != null)
		{
			channel.Subscribe<HealthChangedEvent>(OnHealthChanged);
		}
	}

	private void OnDisable()
	{
		if (channel != null)
		{
			channel.Unsubscribe<HealthChangedEvent>(OnHealthChanged);
		}
	}

	private void OnHealthChanged(HealthChangedEvent e)
	{
		if (_healthBarFill == null || _healthText == null) return;

		float percentage = (e.CurrentHealth / e.MaxHealth) * 100f;
		_healthBarFill.style.width = Length.Percent(percentage);
		_healthText.text = $"{Mathf.CeilToInt(e.CurrentHealth)} / {Mathf.CeilToInt(e.MaxHealth)}";
	}
}
