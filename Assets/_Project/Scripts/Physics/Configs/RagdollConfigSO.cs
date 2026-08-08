using UnityEngine;

[CreateAssetMenu(fileName = "Ragdoll Config", menuName = "Data/Configs/Ragdoll Config")]
public class RagdollConfigSO : ScriptableObject
{
	public float FreezeDelay = 1.5f;
	public float ExcessDamageForceMultiplier = 10f;
}
