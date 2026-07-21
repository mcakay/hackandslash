using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
public class PlayerInputController : MonoBehaviour
{
	[SerializeField] private VelocityMovement velocityMovement;
	[SerializeField] private VelocityRotation velocityRotation;

	[SerializeField] private InputReaderSO _inputReader;

	private LocalEventChannel _channel;

	private int _primaryHash;
	private int _secondaryHash;
	private int _dashHash;
	private int _castHash;
	private int _ultimateHash;

	private bool _isCasting;

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

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		_isCasting = true;
		velocityMovement.SetDirection(Vector2.zero);
		velocityRotation.SetDirection(Vector2.zero);
	}

	private void OnAbilityCastEnded(AbilityCastEndedEvent e)
	{
		_isCasting = false;
	}
}
