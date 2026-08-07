using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessSystem : MonoBehaviour
{
	[Header("Global Volume Settings")]
	[SerializeField] private Volume _globalVolume;

	[Header("Events (Listening)")]
	[SerializeField] private ChromaticAberrationEventChannel _chromaticAberrationEventChannel;
	[SerializeField] private LensDistortionEventChannel _lensDistortionEventChannel;
	[SerializeField] private VignetteEventChannel _vignetteEventChannel;

	private ChromaticAberration _chromaticAberration;
	private LensDistortion _lensDistortion;
	private Vignette _vignette;

	private float _caDecayRate;
	private float _ldDecayRate;
	private float _vignetteDecayRate;

	private float _defaultVignette;

	private void Awake()
	{
		if (_globalVolume.profile.TryGet(out ChromaticAberration ca))
		{
			_chromaticAberration = ca;
		}

		if (_globalVolume.profile.TryGet(out LensDistortion ld))
		{
			_lensDistortion = ld;
		}

		if (_globalVolume.profile.TryGet(out Vignette v))
		{
			_vignette = v;
			_defaultVignette = v.intensity.value;
		}
	}

	private void OnEnable()
	{
		if (_chromaticAberrationEventChannel != null)
		{
			_chromaticAberrationEventChannel.Subscribe(OnChromaticAberrationEvent);
		}

		if (_lensDistortionEventChannel != null)
		{
			_lensDistortionEventChannel.Subscribe(OnLensDistortionEvent);
		}

		if (_vignetteEventChannel != null)
		{
			_vignetteEventChannel.Subscribe(OnVignetteEvent);
		}
	}

	private void OnDisable()
	{
		if (_chromaticAberrationEventChannel != null)
		{
			_chromaticAberrationEventChannel.Unsubscribe(OnChromaticAberrationEvent);
		}

		if (_lensDistortionEventChannel != null)
		{
			_lensDistortionEventChannel.Unsubscribe(OnLensDistortionEvent);
		}

		if (_vignetteEventChannel != null)
		{
			_vignetteEventChannel.Unsubscribe(OnVignetteEvent);
		}
	}

	private void Update()
	{
		if (_chromaticAberration != null && _chromaticAberration.intensity.value > 0f)
		{
			_chromaticAberration.intensity.value -= _caDecayRate * Time.unscaledDeltaTime;

			if (_chromaticAberration.intensity.value < 0f)
			{
				_chromaticAberration.intensity.value = 0f;
			}
		}

		if (_lensDistortion != null && Mathf.Abs(_lensDistortion.intensity.value) > 0f)
		{
			_lensDistortion.intensity.value = Mathf.MoveTowards(
				_lensDistortion.intensity.value,
				0f,
				_ldDecayRate * Time.unscaledDeltaTime
			);
		}

		if (_vignette != null && _vignette.intensity.value > _defaultVignette)
		{
			_vignette.intensity.value = Mathf.MoveTowards(
				_vignette.intensity.value,
				_defaultVignette,
				_vignetteDecayRate * Time.unscaledDeltaTime
			);
		}
	}

	public void OnChromaticAberrationEvent(ChromaticAberrationEventPayload payload)
	{
			_chromaticAberration.intensity.value = payload.Intensity;
			_caDecayRate = payload.Duration > 0f ? payload.Intensity / payload.Duration : 1000f;
	}

	public void OnLensDistortionEvent(LensDistortionEventPayload payload)
	{
			_lensDistortion.intensity.value = payload.Intensity;
			_ldDecayRate = payload.Duration > 0f ? Mathf.Abs(payload.Intensity) / payload.Duration : 1000f;
	}

	public void OnVignetteEvent(VignetteEventPayload payload)
	{
			_vignette.intensity.value = payload.Intensity;
			_vignetteDecayRate = payload.Duration > 0f ? (payload.Intensity - _defaultVignette) / payload.Duration : 1000f;
	}
}
