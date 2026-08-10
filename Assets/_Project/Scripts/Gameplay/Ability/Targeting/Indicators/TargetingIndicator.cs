using UnityEngine;
using UnityEngine.Pool;

public abstract class TargetingIndicator : MonoBehaviour, IPooledObject<TargetingIndicator>
{
	private IObjectPool<TargetingIndicator> _pool;

	public abstract void UpdateAim(Vector3 origin, Vector3 worldPos, float range, float size, float chargeRatio = 1f);

	public void SetPool(IObjectPool<TargetingIndicator> pool)
	{
		_pool = pool;
	}

	public void ReturnToPool()
	{
		if (_pool != null)
		{
			_pool.Release(this);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
