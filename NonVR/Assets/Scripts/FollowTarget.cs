using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Used by AI entities to follow a (potentially) moving target.</summary>
public class FollowTarget : MonoBehaviour
{
    /// <summary>
    /// Is the pet NavMeshAgent currently moving towards it's target?</summary>
    public bool CurrentlyMoving { get { return !agent.isStopped; } set { agent.isStopped = !value; } }

    /// <summary>
    /// GameObject currently being tracked by LateUpdate.</summary>
    public GameObject Target { get; set; } //autoproperty
    private Vector3 previousTargetLocation; //used in lateupdate for checking if target moved.

    /// <summary>
    /// Moves entity across navmesh.</summary>
    private NavMeshAgent agent;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Rotate around y axis to face target.</summary>
    public void FaceTarget()
    {
        transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
    }

    /// <summary>
    /// Checks if target has moved since last update, 
    /// if so, sets navmeshagent target to target's new location.</summary>
    private void LateUpdate()
    {
        if (Target != null)
        {
            Vector3 currentTargetLocation = Target.transform.position;
            if (!currentTargetLocation.Equals(previousTargetLocation))
            {
                agent.SetDestination(currentTargetLocation);
            }
            previousTargetLocation = currentTargetLocation;
        }
    }
}
