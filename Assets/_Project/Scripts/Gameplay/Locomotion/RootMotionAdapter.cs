using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class RootMotionAdapter : MonoBehaviour
{
	[SerializeField] protected Rigidbody rb;

	protected Animator _animator;

	protected virtual void Awake()
	{
		_animator = GetComponent<Animator>();
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

		OnRootMotionApplied(newPosition, newRotation);
	}

	protected virtual void OnRootMotionApplied(Vector3 newPosition, Quaternion newRotation) { }
}
