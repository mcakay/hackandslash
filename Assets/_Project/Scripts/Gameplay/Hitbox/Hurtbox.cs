using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[RequireComponent(typeof(Rigidbody))]
public class Hurtbox : MonoBehaviour
{
	[SerializeField] private float knockbackDuration = 0.15f;
	[SerializeField] private Animator animator;
	private LocalEventChannel _channel;
	private Rigidbody _rigidbody;

	private Timer _knockbackTimer;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();
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

	public void Damage(float damage, float knockbackForce, Vector3 sourcePosition)
	{
		_channel.Publish(new EmissionRequestedEvent());

		//TO DO: transfer push mechanics to a separate component
		animator.applyRootMotion = false;
		_rigidbody.isKinematic = false;
		_rigidbody.linearVelocity = Vector3.zero;
		Vector3 pushDirection = transform.position - sourcePosition;
		pushDirection.y = 0f;
		pushDirection.Normalize();

		animator.SetTrigger("Hit");

		_rigidbody.AddForce(pushDirection * knockbackForce, ForceMode.Impulse);
		_knockbackTimer.Start(knockbackDuration);
	}

	private void Update()
	{
		if (_knockbackTimer.IsRunning)
		{
			_knockbackTimer.Tick(Time.deltaTime);
		}
	}

	private void OnKnockbackFinished()
	{
		animator.applyRootMotion = true;
		_rigidbody.isKinematic = true;
	}

}
