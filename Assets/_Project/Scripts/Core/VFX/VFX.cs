using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

[RequireComponent(typeof(VisualEffect))]
public class VFX : MonoBehaviour, IPooledObject<VFX>
{
	private IObjectPool<VFX> _pool;
	private VisualEffect _vfx;

	[SerializeField] private float lifeTime = 2f;

	private void Awake()
	{
		_vfx = GetComponent<VisualEffect>();
	}

	private void OnEnable()
	{
		if (_vfx != null)
		{
			_vfx.Play();

			Invoke(nameof(ReturnToPool), lifeTime);
		}
	}

	private void OnDisable()
	{
		CancelInvoke(nameof(ReturnToPool));
	}

	public void SetPool(IObjectPool<VFX> pool)
	{
		_pool = pool;
	}

	public void ReturnToPool()
	{
		_pool?.Release(this);
	}

}
