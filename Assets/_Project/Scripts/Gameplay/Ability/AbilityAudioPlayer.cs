using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LocalEventChannel))]
public class AbilityAudioPlayer : MonoBehaviour
{
    [SerializeField] private SFXEventSO sfxEvent;

    private LocalEventChannel _channel;

    private void Awake()
    {
        _channel = GetComponent<LocalEventChannel>();
    }

    private void OnEnable()
    {
        _channel.Subscribe<AbilityExecutionStartedEvent>(OnAbilityExecutionStarted);
        _channel.Subscribe<FirstImpactRegisteredEvent>(OnFirstImpactRegistered);
    }

    private void OnDisable()
    {
        _channel.Unsubscribe<AbilityExecutionStartedEvent>(OnAbilityExecutionStarted);
        _channel.Unsubscribe<FirstImpactRegisteredEvent>(OnFirstImpactRegistered);
    }

    private void OnAbilityExecutionStarted(AbilityExecutionStartedEvent e)
    {
        if (e.Ability.CastSFX != null)
        {
            PlaySFXList(e.Ability.CastSFX, transform.position);
        }
    }

    private void OnFirstImpactRegistered(FirstImpactRegisteredEvent e)
    {
        if (e.ImpactSFX != null)
        {
            PlaySFXList(e.ImpactSFX, e.Position);
        }
    }

    private void PlaySFXList(List<SFXConfig> sfxList, Vector3 position)
    {
        foreach (var sfxLayer in sfxList)
        {
            if (sfxLayer.IsValid())
            {
                sfxEvent.Raise(sfxLayer.CreatePayload(position));
            }
        }
    }
}
