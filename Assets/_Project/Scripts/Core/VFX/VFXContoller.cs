using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VFXController : MonoBehaviour, IPooledObject<VFXController>
{
	private IObjectPool<VFXController> _pool;
	private VisualEffect _vfx;

    [SerializeField] private float lifeTime = 2f;

    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
    }

	public void SetPool(IObjectPool<VFXController> pool)
	{
		_pool = pool;
	}

	public void ReturnToPool()
	{
		_pool?.Release(this);
	}

	private void OnEnable()
    {
        if (_vfx != null)
        {
            _vfx.Play();

            Invoke(nameof(ReturnToPool), lifeTime);
        }
    }

}
