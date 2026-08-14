using System;

[Serializable]
public class MoveToTargetAction : IAction
{
    public void Act(AIStateController controller)
    {
        if (controller.TargetEntity == null)
        {
            controller.NavMeshAgent.isStopped = true;

            if (controller.NavMeshAgent.hasPath)
            {
                controller.NavMeshAgent.ResetPath();
            }
            return;
        }

        controller.NavMeshAgent.isStopped = false;

        if (controller.SelectedAbility != null)
        {
            controller.NavMeshAgent.stoppingDistance = controller.SelectedAbility.Data.Range * 0.9f;
        }

        controller.NavMeshAgent.SetDestination(controller.TargetEntity.Transform.position);
    }
}
