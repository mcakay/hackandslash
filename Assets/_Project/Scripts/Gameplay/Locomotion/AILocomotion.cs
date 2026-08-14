using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterAnimator))]
[RequireComponent(typeof(LocalEventChannel))]
public class AILocomotion : MonoBehaviour
{
	[SerializeField] private float _turnSpeed = 10f;

	private NavMeshAgent _agent;
	private Rigidbody _rb;
	private LocalEventChannel _channel;
	private CharacterAnimator _characterAnimator;

	private void Awake()
	{
		_agent = GetComponent<NavMeshAgent>();
		_rb = GetComponent<Rigidbody>();
		_channel = GetComponent<LocalEventChannel>();
		_characterAnimator = GetComponent<CharacterAnimator>();

		_agent.updatePosition = false;
		_agent.updateRotation = false;
	}

	private void OnEnable()
	{
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	private void Update()
	{
		if (_agent.isStopped || !_agent.enabled || (!_agent.hasPath && !_agent.pathPending))
		{
			_characterAnimator.UpdateLocomotion(0f);
			return;
		}

		_characterAnimator.UpdateLocomotion(1f);
	}

	private void FixedUpdate()
	{
		if (!_agent.isStopped && _agent.enabled && _agent.desiredVelocity.sqrMagnitude > 0.01f)
		{
			Vector3 lookDirection = _agent.desiredVelocity;
			lookDirection.y = 0;

			if (lookDirection != Vector3.zero)
			{
				Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
				_rb.MoveRotation(Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * _turnSpeed));
			}
		}
	}

	private void OnDeath(DeathEvent e)
	{
		if (_agent.isOnNavMesh)
		{
			_agent.isStopped = true;
		}
		_agent.enabled = false;

		this.enabled = false;
	}
}
