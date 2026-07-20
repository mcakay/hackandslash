using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
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
		Move(_direction);
	}

	public void SetDirection(Vector2 input)
	{
		_direction = input;
	}

	private void Move(Vector2 input)
	{
		if (input.sqrMagnitude < 0.01f)
		{
			_rigidbody.linearVelocity = new Vector3(0f, _rigidbody.linearVelocity.y, 0f);
			return;
		}

		Vector3 inputDirection = new(input.x, 0, input.y);
		Vector3 skewedDirection = _isometricMatrix.MultiplyPoint3x4(inputDirection);

		Vector3 targetVelocity = skewedDirection * 5f;
		targetVelocity.y = _rigidbody.linearVelocity.y;

		_rigidbody.linearVelocity = targetVelocity;
	}
}
