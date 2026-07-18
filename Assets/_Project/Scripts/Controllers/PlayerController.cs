using UnityEngine;

public class PlayerController : MonoBehaviour, IInputProvider
{
	[SerializeField] private InputReaderSO inputReader;

	private Vector2 _movementDirection;

	private void OnEnable()
	{
		inputReader.EnableInput();

		inputReader.MovementUpdated += OnMovementUpdated;
	}

	private void OnDisable()
	{
		inputReader.DisableInput();

		inputReader.MovementUpdated -= OnMovementUpdated;
	}

	private void OnMovementUpdated(Vector2 movement)
	{
		_movementDirection = movement;
	}

	public Vector2 GetMovementDirection()
	{
		return _movementDirection;
	}
}

