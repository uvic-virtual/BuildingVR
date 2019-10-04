using System;
using UnityEngine;
using UnityEngine.AI;

public class PetMovement: MonoBehaviour
{
    [SerializeField] private float JumpForce = 250f;
    [SerializeField] private float BodyYDisplacement = 0.42f;

    /// <summary>
    /// Is the pet NavMeshAgent currently moving towards it's target?</summary>
    public bool CurrentlyMoving { get { return !agent.isStopped; } set { agent.isStopped = !value; } }

    /// <summary>
    /// GameObject currently being tracked by Follow routine.</summary>
    public GameObject Target { get; set; } //autoproperty
    private Vector3 previousTargetLocation; //used in lateupdate for checking if target moved.

    /// <summary>
    /// Rigidbody component of pet body. Used for jumping body (but not parent gameobject).</summary>
    private Rigidbody bodyRigidbody;

    /// <summary>
    /// Transform of body. Used to check localposition of body in FixedUpdate.</summary>
    private Transform bodyTransform;

    /// <summary>
    /// NavmeshAgent of Pet.</summary>
    private NavMeshAgent agent;

    /// <summary>
    /// Initialize fields, and sets target to player.</summary>
    private void Start()
    {
        //gets the transform and rigidbody of the pet's body component.
        //GetComponentInChildren can't be used beacuse it checks GetComponent<>() first.
        foreach (Transform child in transform)
        {
            bodyRigidbody = child.GetComponent<Rigidbody>();
            bodyTransform = child.GetComponent<Transform>();
        }
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Controls bouncing of pet.</summary>
    private void FixedUpdate()
    {
        if (bodyTransform.localPosition.y < BodyYDisplacement) //if (roughly) on ground
        {
            StopRigidbody();
            Jump();
        }
    }

    /// <summary>
    /// Controls pathfinding of pet by checking if target has moved.</summary>
    private void LateUpdate()
    {
        try
        {
            Vector3 currentTargetLocation = Target.transform.position;

            if (currentTargetLocation != previousTargetLocation)
            {
                agent.SetDestination(Target.transform.position);
            }
            previousTargetLocation = currentTargetLocation;
        }
        catch (NullReferenceException)
        {
            Debug.Log("No Target!");
        }
    }

    /// <summary>
    /// Stops pet's body's rigidibody component.  Used for predictable jump sizes.</summary>
    private void StopRigidbody()
    {
        bodyRigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Adds force up in one frame to hop.</summary>
    private void Jump()
    {
        float force = JumpForce * Time.fixedDeltaTime;
        bodyRigidbody.AddRelativeForce(0f, force, 0f, ForceMode.Impulse);
    }
}
