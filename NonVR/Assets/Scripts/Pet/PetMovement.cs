using UnityEngine;

/// <summary>
/// Controls what pet is following around.</summary>
public class PetMovement : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float FollowDistance = 4f;

    private FollowTarget followTarget;

    private void Start()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player"); 
        }
        followTarget = GetComponent<FollowTarget>();
        followTarget.Target = Player;
    }

    private void Update()
    {
        //Look at player.
        followTarget.FaceTarget();

        //Check if pet is close to player, and if so halts a short distance away.
        if (Vector3.Distance(transform.position, Player.transform.position) < FollowDistance)
        {
            followTarget.CurrentlyMoving = false;
        }
        else
        {
            followTarget.CurrentlyMoving = true;
        }
    }
}
