using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RootMotionAdapter : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private LocalEventChannel channel;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_animator.applyRootMotion = false;
	}

	private void OnEnable()
	{
		channel.Subscribe<AbilityCastStartedEvent>(OnAbilityStarted);
		channel.Subscribe<AbilityCastEndedEvent>(OnAbilityEnded);
	}

	private void OnDisable()
	{
		channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityStarted);
		channel.Unsubscribe<AbilityCastEndedEvent>(OnAbilityEnded);
	}

	public void ToggleRootMotion(bool state)
	{
		_animator.applyRootMotion = state;
	}

	private void OnAnimatorMove()
	{
		if (_animator == null || !_animator.applyRootMotion)
		{
			return;
		}

		Vector3 newPosition = rb.position + _animator.deltaPosition;
		Quaternion newRotation = rb.rotation * _animator.deltaRotation;

		rb.MovePosition(newPosition);
		rb.MoveRotation(newRotation);
	}

	private void OnAbilityStarted(AbilityCastStartedEvent e) => _animator.applyRootMotion = true;
	private void OnAbilityEnded(AbilityCastEndedEvent e) => _animator.applyRootMotion = false;
}
