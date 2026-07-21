using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LocalEventChannel))]
public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private float dampTime = 0.1f;
	[SerializeField] private Animator animator;

	private int _animSpeedHash;
	private int _velocityXHash;
	private int _velocityZHash;

	private Rigidbody _rigidbody;

	private LocalEventChannel _channel;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_channel = GetComponent<LocalEventChannel>();


		_velocityXHash = Animator.StringToHash("VelocityX");
		_velocityZHash = Animator.StringToHash("VelocityZ");
		_animSpeedHash = Animator.StringToHash("AnimationSpeed");
	}

	private void OnEnable()
	{
		_channel.Subscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
	}

	private void Update()
	{
		Vector3 worldVelocity = _rigidbody.linearVelocity;
		Vector3 localVelocity = transform.InverseTransformDirection(worldVelocity);

		Vector2 planarVelocity = new(localVelocity.x, localVelocity.z);

		if (planarVelocity.magnitude > 0.1f)
		{
			Vector2 normalizedDir = planarVelocity.normalized;
			animator.SetFloat(_velocityXHash, normalizedDir.x, dampTime, Time.deltaTime);
			animator.SetFloat(_velocityZHash, normalizedDir.y, dampTime, Time.deltaTime);
		}
		else
		{
			animator.SetFloat(_velocityXHash, 0f, dampTime, Time.deltaTime);
			animator.SetFloat(_velocityZHash, 0f, dampTime, Time.deltaTime);
		}
	}

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		animator.applyRootMotion = true;
		animator.SetFloat(_animSpeedHash, e.Speed);
		animator.SetTrigger(e.Hash);
	}
}
