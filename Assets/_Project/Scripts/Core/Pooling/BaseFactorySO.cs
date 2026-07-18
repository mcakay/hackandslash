using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseFactorySO<T> : ScriptableObject where T : MonoBehaviour, IPooledObject<T>
{
	[Header("Settings")]
	[SerializeField] private T _prefab;
	[SerializeField] private int _defaultCapacity = 20;
	[SerializeField] private int _maxSize = 100;

	private IObjectPool<T> _pool;

	public IObjectPool<T> Pool
	{
		get
		{
			if (_pool == null)
			{
				_pool = new ObjectPool<T>(
					CreateSetup,
					GetSetup,
					ReleaseSetup,
					DestroySetup,
					false,
					_defaultCapacity,
					_maxSize
				);
			}
			return _pool;
		}
	}

	public T Get(Vector3 position, Quaternion rotation)
	{
		T obj = Pool.Get();
		obj.transform.SetPositionAndRotation(position, rotation);
		return obj;
	}

	private T CreateSetup()
	{
		T obj = Instantiate(_prefab);
		obj.SetPool(Pool);
		return obj;
	}

	private void GetSetup(T obj) => obj.gameObject.SetActive(true);
	private void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
	private void DestroySetup(T obj) => Destroy(obj.gameObject);

	private void OnDisable()
	{
		_pool?.Clear();
		_pool = null;
	}
}
