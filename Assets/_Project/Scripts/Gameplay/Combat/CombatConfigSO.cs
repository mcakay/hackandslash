using UnityEngine;
using Alchemy.Inspector;

[CreateAssetMenu(fileName = "Combat Config", menuName = "Data/Config/Combat")]
public class CombatConfigSO : ScriptableObject
{
    [FoldoutGroup("Knockback Settings")]
    public float GlobalKnockbackDuration = 0.15f;
}
