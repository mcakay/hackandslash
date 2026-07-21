using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VelocityRotation : MonoBehaviour
{
	private Rigidbody _rigidbody;
	private Matrix4x4 _isometricMatrix;

	private Vector2 _direction;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_isometricMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45f, 0));
	}

	private void FixedUpdate()
	{
		Rotate(_direction);
	}

	public void SetDirection(Vector2 input)
	{
		_direction = input;
	}

	private void Rotate(Vector2 input)
	{
		if (input.sqrMagnitude < 0.01f)
		{
			return;
		}

		Vector3 inputDirection = new(input.x, 0, input.y);
		Vector3 skewedDirection = _isometricMatrix.MultiplyPoint3x4(inputDirection);

		var targetRotation = Quaternion.LookRotation(skewedDirection, Vector3.up);
		_rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotation, 10f * Time.fixedDeltaTime));
	}
}
