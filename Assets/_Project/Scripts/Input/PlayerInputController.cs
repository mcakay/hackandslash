using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LocalEventChannel))]
public class PlayerInputController : MonoBehaviour
{
	[SerializeField] private VelocityMovement velocityMovement;
	[SerializeField] private VelocityRotation velocityRotation;

	[SerializeField] private InputReaderSO _inputReader;
	[SerializeField] private TargetingController _targetingController;

	private LocalEventChannel _channel;

	private int _primaryHash;
	private int _secondaryHash;
	private int _dashHash;
	private int _castHash;
	private int _ultimateHash;

	private bool _isCasting;
	private bool _isAiming;
	private int _aimingAbilityHash;

	private Dictionary<int, TargetingSettings> _targetedAbilities = new();

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();

		_primaryHash = Animator.StringToHash("Primary");
		_secondaryHash = Animator.StringToHash("Secondary");
		_dashHash = Animator.StringToHash("Dash");
		_castHash = Animator.StringToHash("Cast");
		_ultimateHash = Animator.StringToHash("Ultimate");
	}

	private void OnEnable()
	{
		if (_inputReader != null)
		{
			_inputReader.EnableInput();

			_inputReader.PrimaryPerformed += OnPrimary;
			_inputReader.SecondaryPerformed += OnSecondary;
			_inputReader.DashPerformed += OnDash;
			_inputReader.CastPerformed += OnCast;
			_inputReader.UltimatePerformed += OnUltimate;
		}

		_channel.Subscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
		_channel.Subscribe<AbilityCastEndedEvent>(OnAbilityCastEnded);
		_channel.Subscribe<TargetingDataUpdatedEvent>(OnTargetingDataUpdated);
	}

	private void OnDisable()
	{
		if (_inputReader != null)
		{
			_inputReader.DisableInput();

			_inputReader.PrimaryPerformed -= OnPrimary;
			_inputReader.SecondaryPerformed -= OnSecondary;
			_inputReader.DashPerformed -= OnDash;
			_inputReader.CastPerformed -= OnCast;
			_inputReader.UltimatePerformed -= OnUltimate;
		}

		_channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
		_channel.Unsubscribe<AbilityCastEndedEvent>(OnAbilityCastEnded);
		_channel.Unsubscribe<TargetingDataUpdatedEvent>(OnTargetingDataUpdated);
	}

	private void Update()
	{
		if (_inputReader == null || _isCasting)
		{
			return;
		}

		Vector2 movementInput = _inputReader.MovementInput;

		velocityMovement.SetDirection(movementInput);
		velocityRotation.SetDirection(movementInput);

		if (_isAiming)
		{
			_targetingController.UpdateAim(_inputReader.PointerPosition);
		}
	}

	private void OnTargetingDataUpdated(TargetingDataUpdatedEvent e)
	{
		_targetedAbilities = e.TargetedAbilities;
	}

	private void OnPrimary()
	{
		if (_isAiming)
		{
			ExecuteAimingAbility();
		}
		else
		{
			ProcessAbilityRequest(_primaryHash);
		}
	}

	private void OnSecondary()
	{
		if (_isAiming)
		{
			CancelAiming();
		}
		else
		{
			ProcessAbilityRequest(_secondaryHash);
		}
	}

	private void OnDash()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_dashHash));
	}

	private void OnCast()
	{
		ProcessAbilityRequest(_castHash);
	}

	private void OnUltimate()
	{
		ProcessAbilityRequest(_ultimateHash);
	}

	private void ProcessAbilityRequest(int hash)
	{
		if (_isAiming)
		{
			if (_aimingAbilityHash == hash)
			{
				CancelAiming();
				return;
			}
			CancelAiming();
		}

		if (_targetedAbilities.TryGetValue(hash, out TargetingSettings settings))
		{
			StartTargeting(hash, settings);
		}
		else
		{
			_channel.Publish(new AbilityCastRequestedEvent(hash));
		}
	}

	private void StartTargeting(int hash, TargetingSettings settings)
	{
		_isAiming = true;
		_aimingAbilityHash = hash;

		_targetingController.StartAiming(settings);

		_targetingController.UpdateAim(_inputReader.PointerPosition);
	}

	private void ExecuteAimingAbility()
	{
		_targetingController.UpdateAim(_inputReader.PointerPosition);
		Vector3 aimDir = _targetingController.AimDirection;

		if (aimDir != Vector3.zero)
		{
			velocityRotation.SnapRotation(aimDir);
		}

		velocityMovement.Stop();
		velocityRotation.Stop();

		velocityMovement.enabled = false;
		velocityRotation.enabled = false;

		CancelAiming();

		_channel.Publish(new AbilityCastRequestedEvent(_aimingAbilityHash));
	}

	private void CancelAiming()
	{
		_isAiming = false;
		_targetingController.StopAiming();
	}

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		_isCasting = true;

		velocityMovement.Stop();
		velocityRotation.Stop();

		velocityMovement.enabled = false;
		velocityRotation.enabled = false;
	}

	private void OnAbilityCastEnded(AbilityCastEndedEvent e)
	{
		_isCasting = false;

		velocityMovement.enabled = true;
		velocityRotation.enabled = true;
	}
}
