using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealRelic : MonoBehaviour
{
    /// <summary>
    /// How high relic is carried above zombie.  Prevents clipping.</summary>
    [SerializeField] private float RelicHeightDisplacement = 1.8f;

    /// <summary>
    /// Potential destination of zombie.</summary>
    private static GameObject relic, temple;

    /// <summary>
    /// gameobject currently being carried by zombie. </summary>
    private GameObject carriedObject;

    /// <summary>
    /// FollowTarget component of this zombie.</summary>
    private FollowTarget movement;

    private void Start()
    {
        if (relic == null)
        {
            relic = GameObject.FindGameObjectWithTag("Relic");
        }

        if (temple == null)
        {
            temple = GameObject.FindGameObjectWithTag("Temple");
        }
        
        movement = GetComponent<FollowTarget>();

        //Set target to relic.
        movement.Target = relic;
    }

    /// <summary>
    /// Drops object being carried by this zombie.
    /// Called in health.Death to prevent relic dissapearing when zombie dies.</summary>
    private void DropObject()
    {
        if (carriedObject != null)
        {
            Rigidbody rigidbody = carriedObject.GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;

            carriedObject.transform.parent = null;
            carriedObject = null;

            //RelicDropped();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(relic))
        {
            PickupObject(relic);
        }
    }


    /// <summary>
    /// Picks up specified gameobject.
    /// (Makes it a child of the zombie and places it above zombie).
    /// </summary>
    /// <param name="other">Gameobject to pickup.</param>
    private void PickupObject(GameObject other)
    {
        carriedObject = other;
        carriedObject.transform.parent = transform;
        carriedObject.transform.localPosition = Vector3.up * RelicHeightDisplacement;
        carriedObject.transform.localRotation = Quaternion.identity;

        Rigidbody rigidbody = carriedObject.GetComponentInChildren<Rigidbody>();
        rigidbody.isKinematic = true;

        //RelicPickedUp();
    }
}
