using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReaderSO : ScriptableObject, InputActions.IPlayerActions
{
	private InputActions _inputActions;

	public event Action<Vector2> MovementUpdated;

	private void OnEnable()
	{
		if (_inputActions == null)
		{
			_inputActions = new InputActions();

			_inputActions.Player.SetCallbacks(this);
		}

		_inputActions.Player.Enable();
	}

	private void OnDisable()
	{
		_inputActions.Player.Disable();
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		MovementUpdated.Invoke(context.ReadValue<Vector2>());
	}
}
