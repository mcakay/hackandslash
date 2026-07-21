using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Input Reader", menuName = "Data/Input/Input Reader")]
public class InputReaderSO : ScriptableObject, InputActions.IPlayerActions
{
	private InputActions _inputActions;

	public Vector2 MovementInput { get; private set; }

	public event Action PrimaryPerformed;
	public event Action SecondaryPerformed;
	public event Action DashPerformed;
	public event Action CastPerformed;
	public event Action UltimatePerformed;

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
		MovementInput = context.ReadValue<Vector2>();
	}

	public void OnPrimary(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			PrimaryPerformed?.Invoke();
		}
	}

	public void OnSecondary(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			SecondaryPerformed?.Invoke();
		}
	}

	public void OnDash(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			DashPerformed?.Invoke();
		}
	}

	public void OnCast(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			CastPerformed?.Invoke();
		}
	}

	public void OnUltimate(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			UltimatePerformed?.Invoke();
		}
	}
}
