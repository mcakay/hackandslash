using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseFactorySO<T> : ScriptableObject, IPoolFactory where T : MonoBehaviour, IPooledObject<T>
{
	[SerializeField] private string factoryName;
	[SerializeField] private T prefab;
	[SerializeField] private int defaultCapacity = 10;
	[SerializeField] private int maxSize = 100;

	private IObjectPool<T> _pool;

	private Vector3 _requestPosition;
	private Quaternion _requestRotation;

	private Transform _poolParent;

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
					true,
					defaultCapacity,
					maxSize
				);
			}
			return _pool;
		}
	}

	public T Get(Vector3 position, Quaternion rotation)
	{
		_requestPosition = position;
		_requestRotation = rotation;

		T obj = Pool.Get();

		obj.transform.SetPositionAndRotation(position, rotation);

		obj.gameObject.SetActive(true);

		return obj;
	}

	public void Prewarm()
	{
		T[] prewarmedObjects = new T[defaultCapacity];

		for (var i = 0; i < defaultCapacity; i++)
		{
			prewarmedObjects[i] = Get(Vector3.zero, Quaternion.identity);
		}

		for (var i = 0; i < defaultCapacity; i++)
		{
			Pool.Release(prewarmedObjects[i]);
		}
	}

	private T CreateSetup()
	{
		T obj = Instantiate(prefab, _requestPosition, _requestRotation, GetPoolParent());

		obj.SetPool(Pool);
		return obj;
	}

	private void GetSetup(T obj)
	{
	}

	private void ReleaseSetup(T obj)
	{
		if (!obj.gameObject.activeSelf)
			return;

		obj.gameObject.SetActive(false);

		if (obj.transform.parent != GetPoolParent())
		{
			obj.transform.SetParent(GetPoolParent());
		}
	}

	private void DestroySetup(T obj)
	{
		if (obj != null)
		{
			Destroy(obj.gameObject);
		}
	}

	private void OnDisable()
	{
		_pool?.Clear();
		_pool = null;
	}

	private Transform GetPoolParent()
	{
		if (_poolParent == null)
		{
			GameObject masterParent = GameObject.Find("Pools");

			if (masterParent == null)
			{
				masterParent = new GameObject("Pools");
			}

			GameObject poolContainer = new($"{factoryName} Pool");

			poolContainer.transform.SetParent(masterParent.transform);

			_poolParent = poolContainer.transform;
		}
		return _poolParent;
	}
}
