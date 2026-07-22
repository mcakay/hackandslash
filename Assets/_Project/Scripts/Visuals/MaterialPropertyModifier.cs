using UnityEngine;

public class MaterialPropertyHandler : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private LocalEventChannel _channel;
	[SerializeField] private Renderer[] _renderers;

	[Header("Colors")]
	[SerializeField] private string _colorPropertyName = "_BaseColor";
	[SerializeField] private Color _baseColor = Color.white;

	[Header("Emission")]
	[SerializeField] private string _emissionPropertyName = "_EmissionColor";
	[SerializeField] private Color _emissionColor = Color.white;
	[SerializeField] private float _emissionIntensity = 2f;
	[SerializeField] private float _emissionDuration = 0.1f;

	private MaterialPropertyBlock _propertyBlock;

	private Timer _flashTimer;

	private void Awake()
	{
		_propertyBlock = new MaterialPropertyBlock();

		if (_renderers == null || _renderers.Length == 0)
		{
			_renderers = GetComponentsInChildren<Renderer>();
		}

		_flashTimer = new Timer();
	}

	private void OnEnable()
	{
		if (_channel != null)
		{
			_channel.Subscribe<EmissionRequestedEvent>(OnEmissionRequested);
		}

		ApplyBaseColor();
		_flashTimer.TimerEnded += StopFlashing;
	}

	private void OnDisable()
	{
		if (_channel != null)
		{
			_channel.Unsubscribe<EmissionRequestedEvent>(OnEmissionRequested);
		}

		_flashTimer.TimerEnded -= StopFlashing;
	}

	private void Update()
	{
		_flashTimer.Tick(Time.deltaTime);
	}

	private void OnEmissionRequested(EmissionRequestedEvent e)
	{
		_flashTimer.Start(_emissionDuration);
		ApplyEmission(_emissionColor, _emissionIntensity);
	}

	private void StopFlashing()
	{
		ApplyBaseColor();
	}

	private void ApplyBaseColor()
	{
		if (_propertyBlock == null || _renderers == null) return;

		foreach (Renderer r in _renderers)
		{
			if (r == null) continue;

			r.GetPropertyBlock(_propertyBlock);
			_propertyBlock.SetColor(_colorPropertyName, _baseColor);
			_propertyBlock.SetColor(_emissionPropertyName, Color.black);
			r.SetPropertyBlock(_propertyBlock);
		}
	}

	private void ApplyEmission(Color color, float intensity)
	{
		if (_propertyBlock == null || _renderers == null) return;

		Color finalEmission = color * Mathf.LinearToGammaSpace(intensity);

		foreach (Renderer r in _renderers)
		{
			if (r == null) continue;

			r.GetPropertyBlock(_propertyBlock);
			_propertyBlock.SetColor(_colorPropertyName, _baseColor);
			_propertyBlock.SetColor(_emissionPropertyName, finalEmission);
			r.SetPropertyBlock(_propertyBlock);
		}
	}
}
