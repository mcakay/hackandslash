using UnityEngine.UIElements;

public class AbilityUISlot
{
    public VisualElement Root { get; private set; }
    public VisualElement CooldownOverlay { get; private set; }
    public Label CooldownText { get; private set; }

    public Ability TrackedAbility { get; set; }

    public AbilityUISlot(VisualElement root)
    {
        Root = root;

        CooldownOverlay = root?.Q<VisualElement>(className: "cooldown-overlay");
        CooldownText = root?.Q<Label>(className: "cooldown-text");
    }
}
