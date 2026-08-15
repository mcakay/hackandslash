using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity Data", menuName = "Data/Entity")]
public class EntityData : ScriptableObject
{
    public List<StatSetup> InitialStats;
    public List<AnimationVariantData> AnimationVariants;
}
