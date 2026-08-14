using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LocalEventChannel))]
public class KnockbackReceiver : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private Rigidbody _rigidbody;
	private Timer _knockbackTimer;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_knockbackTimer = new Timer();
	}

	private void OnEnable()
	{
		_knockbackTimer.TimerEnded += OnKnockbackFinished;
	}

	private void OnDisable()
	{
		_knockbackTimer.TimerEnded -= OnKnockbackFinished;
	}

	private void Update()
	{
		if (_knockbackTimer.IsRunning)
		{
			_knockbackTimer.Tick(Time.deltaTime);
		}
	}

	public void ApplyKnockback(Vector3 direction, float force, float duration)
	{
		animator.applyRootMotion = false;
		_rigidbody.linearVelocity = Vector3.zero;

		_rigidbody.AddForce(direction * force, ForceMode.Impulse);

		_knockbackTimer.Start(duration);
	}

	private void OnKnockbackFinished()
	{
		animator.applyRootMotion = true;
	}
}
