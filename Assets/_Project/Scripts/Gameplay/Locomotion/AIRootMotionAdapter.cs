using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class AIRootMotionAdapter : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private NavMeshAgent agent;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.applyRootMotion = true;
    }

    private void OnAnimatorMove()
    {
        if (_animator == null || rb == null) return;

        Vector3 animVelocity = _animator.velocity;

        animVelocity.y = rb.linearVelocity.y;

        rb.linearVelocity = animVelocity;

        if (agent != null)
        {
            agent.nextPosition = rb.position;
        }
    }
}
