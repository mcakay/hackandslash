using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseFactorySO<T> : ScriptableObject where T : MonoBehaviour, IPooledObject<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private IObjectPool<T> _pool;

    private Vector3 _requestPosition;
    private Quaternion _requestRotation;

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
                    _defaultCapacity,
                    _maxSize
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

    private T CreateSetup()
    {
        T obj = Instantiate(_prefab, _requestPosition, _requestRotation);

        obj.SetPool(Pool);
        return obj;
    }

    private void GetSetup(T obj)
    {
    }

    private void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
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
}
