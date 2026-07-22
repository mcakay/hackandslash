using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
	[Header("SFX Settings")]
	[SerializeField] private int sfxPoolSize = 15;
	[SerializeField] private Transform sfxPoolContainer;

	[Header("Music Settings")]
	[SerializeField] private AudioSource musicSource;

	private readonly Queue<AudioSource> _sfxPool = new();

	private void Awake()
	{
		InitializeSFXPool();
	}

	private void InitializeSFXPool()
	{

		if (sfxPoolContainer == null)
		{
			sfxPoolContainer = this.transform;
		}

		for (int i = 0; i < sfxPoolSize; i++)
		{
			var source = sfxPoolContainer.gameObject.AddComponent<AudioSource>();
			source.playOnAwake = false;
			_sfxPool.Enqueue(source);
		}
	}

	public void PlaySFX(SFXEventPayload payload)
	{
		if (payload.Clip == null) return;

		AudioSource source = GetAvailableSFXSource();
		source.clip = payload.Clip;
		source.volume = payload.Volume;
		source.pitch = payload.Pitch;
		source.Play();
	}

	public void PlayMusic(MusicEventPayload payload)
	{
		if (payload.Clip == null) return;
		if (musicSource.clip == payload.Clip && musicSource.isPlaying) return;

		musicSource.clip = payload.Clip;
		musicSource.volume = payload.Volume;
		musicSource.loop = payload.Loop;
		musicSource.Play();
	}

	private AudioSource GetAvailableSFXSource()
	{
		AudioSource source = _sfxPool.Dequeue();

		if (source.isPlaying)
		{
			source.Stop();
		}

		_sfxPool.Enqueue(source);
		return source;
	}
}
