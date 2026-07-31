using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour, IPooledObject<Projectile>
{
	[SerializeField] private ProjectileSO _data;
	[SerializeField] private TrailRenderer[] trailRenderers;

	private IObjectPool<Projectile> _pool;
	private AbilityEffectPayload _payload;
	private GameObject _caster;
	private bool _isFired;

	private Rigidbody _rb;
	private Collider _col;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_col = GetComponent<Collider>();

		_col.enabled = false;
	}

	private void OnEnable()
	{
		foreach (var trail in trailRenderers)
		{
			if (trail != null)
			{
				trail.Clear();
				trail.emitting = true;
			}
		}
	}

	public void SetPool(IObjectPool<Projectile> pool)
	{
		_pool = pool;
	}

	public void Fire(GameObject caster, AbilityEffectPayload payload)
	{
		_caster = caster;
		_payload = payload;

		_rb.linearVelocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;

		_col.enabled = true;

		var direction = caster.transform.forward;

		_rb.linearVelocity = direction.normalized * _data.Speed;

		_isFired = true;
		Invoke(nameof(ReturnToPool), _data.Lifetime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!_isFired) return;
		if (other.gameObject == _caster || other.transform.root.gameObject == _caster) return;

		if (other.TryGetComponent(out Hurtbox hurtbox))
		{
			hurtbox.ReceiveHit(_payload, transform.position);
			_payload.OnFirstImpact(gameObject, transform.position);

			ReturnToPool();
		}
	}

	public void ReturnToPool()
	{
		_isFired = false;
		CancelInvoke(nameof(ReturnToPool));

		_rb.linearVelocity = Vector3.zero;
		_rb.angularVelocity = Vector3.zero;
		_col.enabled = false;

		foreach (var trail in trailRenderers)
		{
			if (trail != null)
			{
				trail.emitting = false;
			}
		}

		_pool?.Release(this);
	}
}
