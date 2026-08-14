using UnityEngine;

[RequireComponent(typeof(LocalEventChannel))]
[SelectionBase]
public class CharacterAnimator : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private float dampTime = 0.1f;

	private int _animSpeedHash;
	private int _speedHash;
	private int _hitHash;

	private LocalEventChannel _channel;

	private void Awake()
	{
		_channel = GetComponent<LocalEventChannel>();

		_speedHash = Animator.StringToHash("Speed");
		_animSpeedHash = Animator.StringToHash("AnimationSpeed");
		_hitHash = Animator.StringToHash("Hit");

		animator.applyRootMotion = true;
	}

	private void OnEnable()
	{
		_channel.Subscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
		_channel.Subscribe<HitReceivedEvent>(OnHitReceived);
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityCastStarted);
		_channel.Unsubscribe<HitReceivedEvent>(OnHitReceived);
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	public void UpdateLocomotion(float currentSpeed)
	{
		animator.SetFloat(_speedHash, currentSpeed, dampTime, Time.deltaTime);
	}

	private void OnAbilityCastStarted(AbilityCastStartedEvent e)
	{
		animator.SetFloat(_animSpeedHash, e.Speed);
		animator.SetTrigger(e.Ability.Data.AnimationHash);
	}

	private void OnHitReceived(HitReceivedEvent e)
	{
		animator.SetTrigger(_hitHash);
	}

	private void OnDeath(DeathEvent e)
	{
		this.enabled = false;
	}
}
