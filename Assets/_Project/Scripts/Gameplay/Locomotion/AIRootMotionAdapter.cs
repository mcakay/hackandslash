using UnityEngine;
using UnityEngine.AI;

public class AIRootMotionAdapter : RootMotionAdapter
{
	[SerializeField] private NavMeshAgent _agent;

	protected override void Awake()
	{
		base.Awake();

		ToggleRootMotion(true);
	}

	protected override void OnRootMotionApplied(Vector3 newPosition, Quaternion newRotation)
	{
		if (_agent != null)
		{
			_agent.nextPosition = newPosition;
		}
	}
}
