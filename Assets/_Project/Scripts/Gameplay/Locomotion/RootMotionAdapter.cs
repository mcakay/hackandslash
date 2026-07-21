using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RootMotionAdapter : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;

	private Animator _animator;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
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
}
