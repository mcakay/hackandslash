using UnityEngine;

[CreateAssetMenu(fileName = "Ability Config", menuName = "Data/Configs/Ability Config")]
public class AbilityConfigSO : ScriptableObject
{
	public int MaxBufferSize = 3;
	public float BufferClearTime = 0.5f;
}
