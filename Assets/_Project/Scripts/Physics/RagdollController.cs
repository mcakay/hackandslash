using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class RagdollController : MonoBehaviour
{
	[Header("Settings")]
	[SerializeField] private RagdollConfigSO config;

	[Header("References")]
	[SerializeField] private Animator animator;
	[SerializeField] private Transform ragdollRoot;

	private LocalEventChannel _channel;
	private Rigidbody _mainRigidbody;
	private Collider _mainCollider;

	private Rigidbody[] _boneRigidbodies;
	private Collider[] _boneColliders;

	private int _corpseLayer;
	private Timer _freezeTimer;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
		_mainRigidbody = GetComponent<Rigidbody>();
		_mainCollider = GetComponent<Collider>();

		_corpseLayer = LayerMask.NameToLayer("Corpse");

		if (ragdollRoot != null)
		{
			_boneRigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
			_boneColliders = ragdollRoot.GetComponentsInChildren<Collider>();
		}

		_freezeTimer = new Timer();
		_freezeTimer.TimerEnded += OnFreezeTimerEnded;

		DisableRagdoll();
	}

	private void OnEnable()
	{
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	private void EnableRagdoll(DeathEvent e)
	{
		animator.enabled = false;

		if (_mainRigidbody != null) _mainRigidbody.isKinematic = true;
		if (_mainCollider != null) _mainCollider.enabled = false;

		gameObject.layer = _corpseLayer;

		foreach (var col in _boneColliders)
		{
			if (col == _mainCollider) continue;

			col.enabled = true;
			col.isTrigger = false;
			col.gameObject.layer = _corpseLayer;
		}

		foreach (var rb in _boneRigidbodies)
		{
			if (rb == _mainRigidbody) continue;
			rb.isKinematic = false;
		}

		if (e.ExcessDamage > 0)
		{
			ApplyImpactForce(e);
		}

		_freezeTimer.Start(config.FreezeDelay);
	}

	private void DisableRagdoll()
	{
		foreach (var rb in _boneRigidbodies)
		{
			if (rb == _mainRigidbody) continue;
			rb.isKinematic = true;
		}

		foreach (var col in _boneColliders)
		{
			if (col == _mainCollider) continue;
			col.enabled = false;
		}

		animator.enabled = true;
	}

	private void ApplyImpactForce(DeathEvent e)
	{
		Rigidbody closestBone = null;
		float minDistance = float.MaxValue;

		foreach (var rb in _boneRigidbodies)
		{
			if (rb == _mainRigidbody) continue;

			float dist = Vector3.Distance(rb.position, e.HitPosition);
			if (dist < minDistance)
			{
				minDistance = dist;
				closestBone = rb;
			}
		}

		if (closestBone != null)
		{
			Vector3 pushDirection = e.HitDirection.normalized;

			pushDirection += Vector3.up * 0.5f;

			float totalForce = e.ExcessDamage * config.ExcessDamageForceMultiplier;
			closestBone.AddForceAtPosition(pushDirection.normalized * totalForce, e.HitPosition, ForceMode.Impulse);
		}
	}

	private void OnFreezeTimerEnded()
	{
		foreach (var rb in _boneRigidbodies)
		{
			if (rb == _mainRigidbody) continue;
			rb.isKinematic = true;
		}

		foreach (var col in _boneColliders)
		{
			if (col == _mainCollider) continue;
			col.isTrigger = true;
		}
	}

	private void OnDeath(DeathEvent e)
	{
		EnableRagdoll(e);
	}
}
