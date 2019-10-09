using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Zombie
{
    /// <summary>
    /// Used by Fetch and Attack Zombies to follow a (potentially) moving target.</summary>
    public abstract class FollowTarget : MonoBehaviour
    {
        ///<summary>
        ///Longest time zombie will go without checking if target has moved.</summary>
        private const float MaxRecalculatePathInterval = 0.25f;

        /// <summary>
        /// Is the navmeshagent stopped?</summary>
        protected bool AgentIsStopped { get { return agent.isStopped; } }

        /// <summary>
        /// Moves zombie across navmesh.</summary>
        private NavMeshAgent agent;

        /// <summary>
        /// Used to keep track of running Follow coroutine.</summary>
        private Coroutine followRoutine;

        public virtual void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        /// <summary>
        /// Changes target to follow.
        /// </summary>
        /// <param name="target">New target to follow.</param>
        protected void SetTarget(GameObject target)
        {
            if (followRoutine != null)
            {
                StopCoroutine(followRoutine);
            }

            try
            {
                followRoutine = StartCoroutine(Follow(target.transform));
            }
            catch (NullReferenceException) //throwback to before I had prefabs set up
            {
                Debug.Log("error: target is null");
            }
        }

        /// <summary>
        /// Rotate around y axis to face target.</summary>
        protected void FaceGameObject(GameObject target)
        {
            transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        }

        /// <summary>
        /// Stops the NavMeshAgent.</summary>
        protected void StopFollowingTarget()
        {
            agent.isStopped = true;
        }

        /// <summary>
        /// Restarts the NavMeshAgent.</summary>
        protected void StartFollowingTarget()
        {
            agent.isStopped = false;
        }

        /// <summary>
        /// Allows Navmesh agent to follow a moving target.
        /// If the target has moved, it recalculates the path to it.
        /// </summary>
        /// <param name="target">Gameobject to follow.</param>
        /// <returns>Follow Coroutine.</returns>
        private IEnumerator Follow(Transform target)
        {
            Vector3 currentTargetPosition = target.position;
            Vector3 previousTargetPosition = currentTargetPosition;
            agent.SetDestination(currentTargetPosition);

            while (true)
            {
                currentTargetPosition = target.position;
                if (!previousTargetPosition.Equals(currentTargetPosition))
                {
                    agent.SetDestination(currentTargetPosition);
                    previousTargetPosition = currentTargetPosition;
                }
                yield return new WaitForSeconds(UnityEngine.Random.Range(0.1f, MaxRecalculatePathInterval));
            }
        }
    }
}
