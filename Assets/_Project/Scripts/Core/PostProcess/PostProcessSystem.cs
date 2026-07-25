using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessSystem : MonoBehaviour
{
	[Header("Global Volume Settings")]
	[SerializeField] private Volume _globalVolume;

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

	public void OnPostProcessEffectEvent(PostProcessEventPayload payload)
	{
		if (payload.UseChromaticAberration && _chromaticAberration != null)
		{
			_chromaticAberration.intensity.value = payload.CAIntensity;
			_caDecayRate = payload.CADuration > 0f ? payload.CAIntensity / payload.CADuration : 1000f;
		}

		if (payload.UseLensDistortion && _lensDistortion != null)
		{
			_lensDistortion.intensity.value = payload.LDIntensity;
			_ldDecayRate = payload.LDDuration > 0f ? Mathf.Abs(payload.LDIntensity) / payload.LDDuration : 1000f;
		}

		if (payload.UseVignette && _vignette != null)
		{
			_vignette.intensity.value = payload.VigIntensity;
			float diff = payload.VigIntensity - _defaultVignette;
			_vignetteDecayRate = payload.VigDuration > 0f ? diff / payload.VigDuration : 1000f;
		}
	}
}
