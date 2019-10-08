using UnityEngine;

namespace Zombie
{
    public class PetMovement : FollowTarget
    {
        [SerializeField] private float JumpForce = 500f;
        [SerializeField] private float BodyYDisplacement = 0.42f;
        [SerializeField] private float FollowDistance = 3f;

        private static GameObject player;

        /// <summary>
        /// Rigidbody component of pet body. Used for jumping body but not parent.</summary>
        private Rigidbody bodyRigidbody;
      
        /// <summary>
        /// Transform of body. Used to check localposition of body in FixedUpdate.</summary>
        private Transform bodyTransform;

        /// <summary>
        /// Initialize fields, and sets target to player.</summary>
        public override void  Start()
        {
            base.Start();
            
            //gets the transform and rigidbody of the pet's body component.
            //GetComponentInChildren can't be used beacuse it checks GetComponent<>() first.
            foreach (Transform child in transform)
            {
                bodyRigidbody = child.GetComponent<Rigidbody>();
                bodyTransform = child.GetComponent<Transform>();
            }

            //checks if static player reference is null, and finds player if it is.
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            SetTarget(player);
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

        /// <summary>
        /// Controls bouncing and forward movement of pet.</summary>
        private void FixedUpdate()
        {
            bool closeToPlayer = Vector3.Distance(player.transform.position, transform.position) < FollowDistance;
            //if (closeToPlayer && !AgentIsStopped) //if close to player and not stopped
            //{
            //    StopFollowingTarget();
            //}
            //else if (!closeToPlayer && AgentIsStopped) //if player isn't close and stopped
            //{
            //    StartFollowingTarget();
            //}

            //if (bodyTransform.localPosition.y < BodyYDisplacement) //if (roughly) on ground
            //{
            //    StopRigidbody();
            //    Jump();
            //}

            if (closeToPlayer)
            {
                FaceGameObject(player);
                
                if (!AgentIsStopped)
                {
                    StopFollowingTarget();
                }
            }
            else if (AgentIsStopped) //if not close to player but agent is stopped
            {
                StartFollowingTarget();
            }

            if (bodyTransform.localPosition.y < BodyYDisplacement) //if (roughly) on ground
            {
                StopRigidbody();
                Jump();
            }
        }
    }
}


