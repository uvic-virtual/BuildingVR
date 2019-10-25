using UnityEngine;
using System.Collections;
using Health;

namespace Zombie
{
    /// <summary>
    /// Used by AttackZombie to follow and attack player.</summary>
    public class AttackPlayer : FollowTarget
    {
        /// <summary>
        /// Radius in which player zombie will stop following and attack player.</summary>
        [SerializeField] private float AttackRadius = 2.0f;

        private static GameObject player;
        private static IDamageable playerHealth;

        /// <summary>
        /// keeps track of damage coroutine.</summary>
        private Coroutine damageRoutine;
        private bool damageRoutineIsRunning;

        /// <summary>
        /// Gets components and sets target to player.</summary>
        public override void Start()
        {
            base.Start();

            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                playerHealth = player.GetComponentInChildren<IDamageable>();
            }
            SetTarget(player);
        }

        /// <summary>
        /// Decreses zombie count by 1.</summary>
        void OnDisable()
        {
            SpawnPad.numAttackZombies--;
        }

        /// <summary>
        /// Decides if the zombie should move and if it should attack the player using this loigc:
        ///     if (closeToplayer){
        ///        1: stop
        ///        2: attack }
        ///     else {
        ///        1: move towards player
        ///        2: stop attacking }
        /// </summary>
        void Update()
        {
            bool closeToPlayer = Vector3.Distance(transform.position, player.transform.position) < AttackRadius;
            if (closeToPlayer)
            {
                FaceGameObject(player);

                if (!AgentIsStopped)
                {
                    StopFollowingTarget();
                }

                if (!damageRoutineIsRunning)
                {
                    damageRoutine = StartCoroutine(Damage(25, 1));
                    damageRoutineIsRunning = true;
                }
            }
            else //if not close to player
            {
                if (damageRoutineIsRunning)
                {
                    StopCoroutine(damageRoutine);
                    damageRoutineIsRunning = false;
                }

                if (AgentIsStopped)
                {
                    StartFollowingTarget();
                }
            }
        }

        /// <summary>
        /// Removes health every iteration.
        /// </summary>
        /// <param name="damagePerDelay">How much health is taken off per iteration.</param>
        /// <param name="timeDelay">Length of delay between iterations in seconds.</param>
        /// <returns>Returns coroutine.</returns>
        private IEnumerator Damage(int damagePerDelay, float timeDelay)
        {
            while (playerHealth.Health > 0)
            {
                playerHealth.Hit(damagePerDelay);
                Debug.Log(playerHealth.Health);
                yield return new WaitForSeconds(timeDelay);
            }
            playerHealth.Kill();
            yield return null;
        }
    }
}
