using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LocalEventChannel))]
public class KnockbackReceiver : MonoBehaviour
{
    [SerializeField] private CombatConfigSO _combatConfig;
    [SerializeField] private Animator animator;

    private LocalEventChannel _channel;
    private Rigidbody _rigidbody;
    private Timer _knockbackTimer;

    private void Awake()
    {
        _channel = GetComponent<LocalEventChannel>();
        _rigidbody = GetComponent<Rigidbody>();
        _knockbackTimer = new Timer();
    }

    private void OnEnable()
    {
        _channel.Subscribe<HitReceivedEvent>(OnHitReceived);
        _knockbackTimer.TimerEnded += OnKnockbackFinished;
    }

    private void OnDisable()
    {
        _channel.Unsubscribe<HitReceivedEvent>(OnHitReceived);
        _knockbackTimer.TimerEnded -= OnKnockbackFinished;
    }

    private void Update()
    {
        if (_knockbackTimer.IsRunning)
        {
            _knockbackTimer.Tick(Time.deltaTime);
        }
    }

    private void OnHitReceived(HitReceivedEvent e)
    {
        if (e.Payload.KnockbackForce <= 0f) return;

        animator.applyRootMotion = false;
        _rigidbody.isKinematic = false;
        _rigidbody.linearVelocity = Vector3.zero;

        Vector3 pushDirection = transform.position - e.SourcePosition;
        pushDirection.y = 0f;
        pushDirection.Normalize();

        _rigidbody.AddForce(pushDirection * e.Payload.KnockbackForce, ForceMode.Impulse);

        _knockbackTimer.Start(_combatConfig.GlobalKnockbackDuration);
    }

    private void OnKnockbackFinished()
    {
        animator.applyRootMotion = true;
        _rigidbody.isKinematic = true;
    }
}
