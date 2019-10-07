using UnityEngine;

/// <summary>
/// Controls bouncing of pet body.</summary>
public class PetBounce : MonoBehaviour
{
    [SerializeField] private float JumpForce = 250f;

    private Rigidbody bodyRigidbody;

	private void Start ()
    {
        bodyRigidbody = GetComponent<Rigidbody>();
	}

    private void OnCollisionEnter()
    {
        //Stops pet body
        bodyRigidbody.velocity = Vector3.zero;

        //What do you think this does?
        Jump();
    }

    /// <summary>
    /// Adds force up in one frame to hop.  </summary>
    private void Jump()
    {
        float force = JumpForce * Time.fixedDeltaTime;
        bodyRigidbody.AddRelativeForce(0f, force, 0f, ForceMode.Impulse);
    }
}
