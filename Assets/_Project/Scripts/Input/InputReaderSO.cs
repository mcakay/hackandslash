using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Input/Input Reader")]
public class InputReaderSO : ScriptableObject, InputActions.IPlayerActions
{
	private InputActions _inputActions;

	public event Action<Vector2> MovementUpdated;

	public void EnableInput()
	{
		if (_inputActions == null)
		{
			_inputActions = new InputActions();
			_inputActions.Player.SetCallbacks(this);
		}

		_inputActions.Player.Enable();
	}

	public void DisableInput()
	{
		if (_inputActions == null)
		{
			return;
		}
		_inputActions.Player.Disable();
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		MovementUpdated?.Invoke(context.ReadValue<Vector2>());
	}
}
