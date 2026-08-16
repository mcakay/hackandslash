using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[RequireComponent(typeof(UIDocument))]
public class HUDAbility : MonoBehaviour
{
    [SerializeField] private LocalEventChannel channel;

    private UIDocument _document;

    private readonly int _primaryHash = Animator.StringToHash("Primary");
    private readonly int _secondaryHash = Animator.StringToHash("Secondary");
    private readonly int _dashHash = Animator.StringToHash("Dash");
    private readonly int _eHash = Animator.StringToHash("Cast");
    private readonly int _rHash = Animator.StringToHash("Ultimate");

    private Dictionary<int, AbilityUISlot> _slots;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        var root = _document.rootVisualElement;

        _slots = new Dictionary<int, AbilityUISlot>
        {
            { _primaryHash, new AbilityUISlot(root.Q<VisualElement>("Ability-Primary")) },
            { _secondaryHash, new AbilityUISlot(root.Q<VisualElement>("Ability-Secondary")) },
            { _dashHash, new AbilityUISlot(root.Q<VisualElement>("Ability-Dash")) },
            { _eHash, new AbilityUISlot(root.Q<VisualElement>("Ability-Cast")) },
            { _rHash, new AbilityUISlot(root.Q<VisualElement>("Ability-Ultimate")) }
        };
    }

    private void OnEnable()
    {
        if (channel != null)
        {
            channel.Subscribe<MovesetUpdatedEvent>(OnMovesetUpdated);
        }
    }

    private void OnDisable()
    {
        if (channel != null)
        {
            channel.Unsubscribe<MovesetUpdatedEvent>(OnMovesetUpdated);
        }
    }

    private void OnMovesetUpdated(MovesetUpdatedEvent e)
    {
        foreach (var kvp in _slots)
        {
            int hash = kvp.Key;
            AbilityUISlot slot = kvp.Value;

            if (e.Abilities.TryGetValue(hash, out var abilityList) && abilityList.Count > 0)
            {
                var firstAbility = abilityList[0];
                slot.TrackedAbility = firstAbility;

                if (firstAbility != null && firstAbility.Data != null && firstAbility.Data.Icon != null)
                {
                    slot.Root.style.backgroundImage = new StyleBackground(firstAbility.Data.Icon);
                }
            }
            else
            {
                slot.TrackedAbility = null;
                slot.Root.style.backgroundImage = null;

                if (slot.CooldownOverlay != null) slot.CooldownOverlay.style.height = Length.Percent(0);
                if (slot.CooldownText != null) slot.CooldownText.text = string.Empty;
            }
        }
    }

    private void Update()
    {
        if (_slots == null) return;

        foreach (var slot in _slots.Values)
        {
            if (slot.TrackedAbility == null) continue;

            if (!slot.TrackedAbility.IsReady)
            {
                float remaining = slot.TrackedAbility.CooldownRemaining;
                float total = slot.TrackedAbility.Data.Cooldown;

                if (slot.CooldownOverlay != null)
                {
                    slot.CooldownOverlay.style.height = Length.Percent((remaining / total) * 100f);
                }

                if (slot.CooldownText != null)
                {
                    slot.CooldownText.text = remaining.ToString("F1");
                }
            }
            else
            {
                if (slot.CooldownOverlay != null && slot.CooldownOverlay.style.height != Length.Percent(0))
                {
                    slot.CooldownOverlay.style.height = Length.Percent(0);
                }

                if (slot.CooldownText != null && slot.CooldownText.text != string.Empty)
                {
                    slot.CooldownText.text = string.Empty;
                }
            }
        }
    }
}
