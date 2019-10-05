using UnityEngine;
using Health;

namespace Zombie
{
    /// <summary>
    /// Used by Fetch zombie to navigate to relic.</summary>
    public class StealRelic : FollowTarget
    {
        /// <summary>
        /// Distance relic is held above zombie (prevents clipping).</summary>
        private const float RelicDisplacement = 1.8f;
        /// <summary>
        /// Origintal rotation of relic.(TODO: fix rotation in blender and remove this)</summary>
        private Quaternion RelicRotation;

        /// <summary>
        /// Delegate for BallPickedUp event.</summary>
        public delegate void BallEvent();
        /// <summary>
        /// Static event, called when a zombie picks up a ball. </summary>
        public static event BallEvent BallPickedUp;

        private static GameObject relic, portal;
        private GameObject carryingObject;
        private HealthManager healthComponent; //contains Death event.

        public override void Start()
        {
            base.Start();
            InitializeObjects();

            RelicRotation = relic.transform.rotation;
            SetTarget(relic);

            healthComponent = GetComponent<HealthManager>();
            healthComponent.Death += DropObject; //drops object when zombie is killed.

            BallPickedUp += OnBallPickedUp;
        }

        /// <summary>
        /// Checks if static members relic and portal are null, and finds them using tags if they are.</summary>
        private void InitializeObjects()
        {
            if (relic == null)
            {
                relic = GameObject.FindGameObjectWithTag("Relic");
            }
            if (portal == null)
            {
                portal = GameObject.FindGameObjectWithTag("Portal");
            }
        }

        /// <summary>
        /// Removes parent zombie from zombie count and removes delegates from events. </summary>
        void OnDisable()
        {
            healthComponent.Death -= DropObject;
            BallPickedUp -= OnBallPickedUp;
            SpawnPad.numFetchZombies--;
        }

        /// <summary>
        /// Checks if object collided into is the relic, if it is picks it up.
        /// </summary>
        /// <param name="col">This collision.</param>
        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.Equals(relic))
            {
                PickupObject(relic);
                BallPickedUp();
            }
        }

        /// <summary>
        /// Changes target to portal.
        /// Delegate added to static BallEvent,
        /// so that all zombies change their targets when a ball is picked up.</summary>
        private void OnBallPickedUp()
        {
            SetTarget(portal);
        }

        /// <summary>
        /// Makes object child of zombie, 
        /// and adjusts it's localposition so that zombie is carriny it.
        /// </summary>
        /// <param name="obj">Object to be picked up</param>
        private void PickupObject(GameObject obj)
        {
            carryingObject = obj;

            Rigidbody rigidbody = carryingObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;

            carryingObject.transform.parent = transform;
            carryingObject.transform.localPosition = Vector3.up * RelicDisplacement;
            carryingObject.transform.localRotation = RelicRotation;
        }

        /// <summary>
        /// Drop (deparent) gameobject being carried.</summary>
        private void DropObject()
        {
            if (carryingObject != null)
            {
                Rigidbody rigidbody = carryingObject.GetComponentInChildren<Rigidbody>();
                rigidbody.isKinematic = false;

                carryingObject.transform.parent = null;
                carryingObject = null;
            }
        }
    }
}