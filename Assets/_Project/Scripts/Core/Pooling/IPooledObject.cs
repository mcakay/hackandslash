using UnityEngine.Pool;

public interface IPooledObject<T> where T : class
{
	void SetPool(IObjectPool<T> pool);
	void ReturnToPool();
}
