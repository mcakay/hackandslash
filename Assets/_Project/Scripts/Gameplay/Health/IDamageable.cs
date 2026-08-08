using UnityEngine;

public interface IDamageable
{
	void TakeDamage(float damage, Vector3 hitPosition, Vector3 hitDirection);
}
