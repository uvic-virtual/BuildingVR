using UnityEngine;

public class Fetch : MonoBehaviour
{
    /// <summary>
    /// Height ball is held above pet</summary>
    [SerializeField] private float BallDisplacement = 2f;

    /// <summary>
    /// How far the ball can get away from the player before the pet chases it.</summary>
    [SerializeField] private float FollowDistance = 5f;

    /// <summary>
    /// Force applied to ball when thrown. </summary>
    [SerializeField] private float ThrowForce = 250f;

    [SerializeField] private GameObject player;

    //ball stuff.
    [SerializeField] private GameObject ball;
    private Rigidbody ballRigidbody;
    private bool carryingBall;

    /// <summary>
    /// PetMovement component of pet</summary>
    private PetMovement movement;
	private void Start()
    {
        movement = GetComponent<PetMovement>(); 
        ballRigidbody = ball.GetComponent<Rigidbody>();
        movement.Target = ball;
	}

    /// <summary>
    /// Determines if pet should chase after ball or player, or if ball should be thrown.
    /// </summary>
    private void FixedUpdate()
    {
        
        /* Logic of this stuff:
         *      if the player isn't close to the ball, and not carring the ball:
         *          go to the ball
         *      else go to the player
         * 
         *      if close to the player and following the player:
         *          Face to player
         *          stop moving
         *          if carrying the ball:
         *              throw the ball
         *      else start moving */
         
        bool closeToPlayer = Vector3.Distance(player.transform.position, transform.position) < FollowDistance;
        bool ballCloseToPlayer = Vector3.Distance(player.transform.position, ball.transform.position) < FollowDistance;

        if (!ballCloseToPlayer && !carryingBall)
        {
            if (!movement.Target.Equals(ball))
            {
                movement.Target = ball;
            }
        }
        else if (!movement.Target.Equals(player)) //if ball is close to player or carrying the ball
        {
            movement.Target = player;
        }

        if (closeToPlayer && movement.Target.Equals(player)) //close to player and following it
        {
            FaceGameObject(player);
            movement.CurrentlyMoving = false;

            if (carryingBall)
            {
                ThrowBall();
            }
        }
        else
        {
            movement.CurrentlyMoving = true;
        }
    }

    /// <summary>
    /// Detects if the trigger box collider is touching the ball. If so, pick up the ball.
    /// </summary>
    /// <param name="other"> Gameobject in trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.Equals(ball))
        {
            PickUpBall();
        }
    }

    /// <summary>
    /// Makes ball child of pet.</summary>
    private void PickUpBall()
    {
        ball.transform.parent = gameObject.transform;
        ballRigidbody.isKinematic = true;

        //place ball above pet
        ball.transform.localPosition = Vector3.up * BallDisplacement;
        ball.transform.localRotation = Quaternion.identity;

        carryingBall = true;
    }


    /// <summary>
    /// Deparents ball, and adds forwards and upwards force to it's rigidbody.</summary>
    private void ThrowBall()
    {
        //deparents ball
        ball.transform.parent = null; 

        //add force to ball rigidbody
        ballRigidbody.isKinematic = false;
        float force = ThrowForce * Time.fixedDeltaTime;
        ballRigidbody.AddRelativeForce(0f, force, force, ForceMode.Impulse);

        carryingBall = false;
    }

    /// <summary>
    /// Rotate around y axis to face target.</summary>
    private void FaceGameObject(GameObject target)
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
    }
}
