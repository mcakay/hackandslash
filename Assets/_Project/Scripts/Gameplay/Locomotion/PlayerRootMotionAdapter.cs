using UnityEngine;

public class PlayerRootMotionAdapter : RootMotionAdapter
{
	[SerializeField] private LocalEventChannel channel;

	protected override void Awake()
	{
		base.Awake();
		_animator.applyRootMotion = false;
	}

	private void OnEnable()
	{
		channel.Subscribe<AbilityCastStartedEvent>(OnAbilityStarted);
		channel.Subscribe<AbilityCastEndedEvent>(OnAbilityEnded);
	}

	private void OnDisable()
	{
		channel.Unsubscribe<AbilityCastStartedEvent>(OnAbilityStarted);
		channel.Unsubscribe<AbilityCastEndedEvent>(OnAbilityEnded);
	}

	private void OnAbilityStarted(AbilityCastStartedEvent e) => _animator.applyRootMotion = true;
	private void OnAbilityEnded(AbilityCastEndedEvent e) => _animator.applyRootMotion = false;

	protected override void OnRootMotionApplied(Vector3 newPosition, Quaternion newRotation) { }
}
