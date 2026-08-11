using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIStateController : MonoBehaviour
{
    [Header("State Machine")]
    [SerializeField] private AIState _currentState;
    [SerializeField] private AIState _remainInState;

    [Header("AI Settings & References")]
    public Transform target;
    public float sightRange = 10f;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

		navMeshAgent.updatePosition = false;
		navMeshAgent.updateRotation = false;
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void TransitionToState(AIState nextState)
    {
        if (nextState != null && nextState != _remainInState)
        {
            _currentState = nextState;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
