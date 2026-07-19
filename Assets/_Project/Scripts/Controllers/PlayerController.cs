using UnityEngine;

[RequireComponent(typeof(Loadout))]
[RequireComponent(typeof(AbilityRunner))]
public class PlayerController : MonoBehaviour, IInputProvider
{
	[SerializeField] private InputReaderSO inputReader;

	private Vector2 _movementDirection;

	private int _primaryAbilityId;
	private int _secondaryAbilityId;
	private int _dashAbilityId;
	private int _castAbilityId;
	private int _ultimateAbilityId;

	private Loadout _loadout;
	private AbilityRunner _abilityRunner;

	private void Awake()
	{
		_loadout = GetComponent<Loadout>();
		_abilityRunner = GetComponent<AbilityRunner>();

		_primaryAbilityId = Animator.StringToHash("Primary");
		_secondaryAbilityId = Animator.StringToHash("Secondary");
		_dashAbilityId = Animator.StringToHash("Dash");
		_castAbilityId = Animator.StringToHash("Cast");
		_ultimateAbilityId = Animator.StringToHash("Ultimate");
	}

	private void OnEnable()
	{
		if (inputReader != null)
		{
			inputReader.EnableInput();

			inputReader.MovementUpdated += OnMovementUpdated;
			inputReader.PrimaryPerformed += OnPrimaryPerformed;
			inputReader.SecondaryPerformed += OnSecondaryPerformed;
			inputReader.DashPerformed += OnDashPerformed;
			inputReader.CastPerformed += OnCastPerformed;
			inputReader.UltimatePerformed += OnUltimatePerformed;
		}

		if (_loadout != null)
		{
			_loadout.OnWeaponEquipped += OnWeaponEquipped;
		}
	}

	private void OnDisable()
	{
		if (inputReader != null)
		{

			inputReader.DisableInput();

			inputReader.MovementUpdated -= OnMovementUpdated;
			inputReader.PrimaryPerformed -= OnPrimaryPerformed;
			inputReader.SecondaryPerformed -= OnSecondaryPerformed;
			inputReader.DashPerformed -= OnDashPerformed;
			inputReader.CastPerformed -= OnCastPerformed;
			inputReader.UltimatePerformed -= OnUltimatePerformed;
		}

		if (_loadout != null)
		{
			_loadout.OnWeaponEquipped -= OnWeaponEquipped;
		}
	}

	public Vector2 GetMovementDirection()
	{
		return _movementDirection;
	}

	private void OnMovementUpdated(Vector2 movement)
	{
		_movementDirection = movement;
	}

	private void OnPrimaryPerformed()
	{
		_abilityRunner.BufferAbility(_primaryAbilityId);
	}

	private void OnSecondaryPerformed()
	{
		_abilityRunner.BufferAbility(_secondaryAbilityId);
	}

	private void OnDashPerformed()
	{
		_abilityRunner.BufferAbility(_dashAbilityId);
	}

	private void OnCastPerformed()
	{
		_abilityRunner.BufferAbility(_castAbilityId);
	}

	private void OnUltimatePerformed()
	{
		_abilityRunner.BufferAbility(_ultimateAbilityId);
	}

	private void OnWeaponEquipped(WeaponSO weapon)
	{
		_abilityRunner.SetMoveset(weapon.Moveset);

	}
}

