using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(LocalEventChannel))]
public class AIStateController : MonoBehaviour
{
	[Header("Core State")]
	[SerializeField] private AIState currentState;

	public NavMeshAgent NavMeshAgent { get; private set; }
	public AbilityController AbilityController { get; private set; }
	public Entity Entity { get; private set; }

	public Entity TargetEntity { get; set; }
	public Ability SelectedAbility { get; set; }
	public float NextThinkTime { get; set; }

	private LocalEventChannel _channel;

	private void Awake()
	{
		NavMeshAgent = GetComponent<NavMeshAgent>();
		AbilityController = GetComponent<AbilityController>();
		Entity = GetComponent<Entity>();

		_channel = GetComponent<LocalEventChannel>();
	}

	private void OnEnable()
	{
		_channel.Subscribe<DeathEvent>(OnDeath);
	}

	private void OnDisable()
	{
		_channel.Unsubscribe<DeathEvent>(OnDeath);
	}

	private void Update()
	{
		if (currentState != null)
		{
			currentState.UpdateState(this);
		}
	}

	public void TransitionToState(AIState nextState)
	{
		if (nextState != currentState)
		{
			currentState = nextState;
		}
	}

	private void OnDeath(DeathEvent e)
	{
		this.enabled = false;
	}
}
