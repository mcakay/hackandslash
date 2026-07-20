using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private Movement movement;
	[SerializeField] private MovementRotation movementRotation;

	[SerializeField] private InputReaderSO _inputReader;

	private LocalEventChannel _channel;

	private int _primaryHash;
	private int _secondaryHash;
	private int _dashHash;
	private int _castHash;
	private int _ultimateHash;

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
	}

	private void Update()
	{
		if (_inputReader == null)
		{
			return;
		}

		Vector2 movementInput = _inputReader.MovementInput;

		if (movement)
		{
			movement.SetDirection(movementInput);
		}

		if (movementRotation)
		{
			movementRotation.SetDirection(movementInput);
		}
	}

	private void OnPrimary()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_primaryHash));
	}

	private void OnSecondary()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_secondaryHash));
	}

	private void OnDash()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_dashHash));
	}

	private void OnCast()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_castHash));
	}

	private void OnUltimate()
	{
		_channel.Publish(new AbilityCastRequestedEvent(_ultimateHash));
	}
}
