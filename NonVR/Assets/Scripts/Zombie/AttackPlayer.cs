using System.Collections;
using UnityEngine;

/// <summary>
/// Used by Attack Zombie to attack player.</summary>
public class AttackPlayer : MonoBehaviour
{
    /// <summary>
    /// How close the zombie has to be to attack the player.</summary>
    [SerializeField] private float AttackRadius = 2.0f;

    /// <summary>
    /// Time between iterations in DamagePlayer routine.</summary>
    [SerializeField] private float AttackTimeDelay = 0.5f;

    /// <summary>
    /// Health taken off player in DamagePlayer routine.</summary>
    [SerializeField] private int DamagePerDelay = 1;

    /// <summary>
    /// FollowTarget component of zombie.</summary>
    private FollowTarget movement;

    /// <summary>
    /// What do you honestly think?</summary>
    private static GameObject player;

    /// <summary>
    /// Damage interface of player's HealthManager component.</summary>
    private static IDamageable playerHealth;

    //Keeps track of damage coroutine.
    private Coroutine damageRoutine;
    private bool damageRoutineRunning;

	private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerHealth = player.GetComponentInChildren<IDamageable>();
        }

        //set target to player.
        movement = GetComponent<FollowTarget>();
        movement.Target = player;
	}

    /// <summary>
    /// Checks if close to player and does stuff accordingly.</summary>
    private void Update()
    {
        /* 
         * Logic of this:
         *  if close to player:
         *      Look at player
         *      Stop moving
         *      Damage Player
         *  if not close to player:
         *      Start Moving 
         *      Stop Damaging 
         */
        //bool closeToPlayer = Vector3.Distance(transform.position, player.transform.position) < AttackRadius;
        //if (closeToPlayer)
        //{
        //    movement.FaceTarget();
        //    movement.CurrentlyMoving = false;

        //    if (!damageRoutineRunning)
        //    {
        //        damageRoutine = StartCoroutine(DamagePlayer(DamagePerDelay, AttackTimeDelay));
        //    }
        //}
        //else //if not close to player:
        //{
        //    movement.CurrentlyMoving = true;

        //    if (damageRoutineRunning)
        //    {
        //        StopCoroutine(damageRoutine);
        //        damageRoutineRunning = false;
        //    }
        //}
    }

    /// <summary>
    /// Takes off an amount of health per timed iteration.
    /// </summary>
    /// <param name="damagePerDelay">Amount of damage taken off per iteration.</param>
    /// <param name="timeDelay">Time between iterations.</param>
    /// <returns></returns>
    private IEnumerator DamagePlayer(int damagePerDelay, float timeDelay)
    {
        damageRoutineRunning = true;
  
        while (playerHealth.Health > 0)
        {
            playerHealth.Hit(damagePerDelay);
            Debug.Log(playerHealth.Health);
            yield return new WaitForSeconds(timeDelay);
        }

        damageRoutineRunning = false;
    }
}
