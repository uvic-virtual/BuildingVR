using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//implement dynamic look_offset
public class LookAt : MonoBehaviour {
    public GameObject Target;
    //length of ray cast
    public float Distance = 50;
    public float Look_Offset = 20;
    public float Distance_Offset = 0.7f;

    private Pet_UI Pet;

    void Start()
    {
        Pet = GameObject.FindObjectOfType<Pet_UI>();
    }

    void Update () {
        //the position the gameObject(camera) is at
        Vector3 hostPos = gameObject.transform.position;
        Vector3 targetPos = Target.transform.position;

        RaycastHit hitinfo;
        //When normalized, a vector keeps the same direction but its length is 1.0.
        //a vector that points at the target from the host's position
        Vector3 ray_destination = (targetPos - hostPos).normalized * Distance;

        //shot a ray from the camera to the target
        Ray ray = new Ray(hostPos, ray_destination);

        //if hit anything
        if (Physics.Raycast(ray, out hitinfo, Distance))
        {
            //if hit the target(with collider), which mean there's nothing blocking the filed of view
            if (hitinfo.collider.gameObject == Target)
            {
                //The angle in degrees between the vector |that's pointing the target from the camera| and |the vector camera's facing|
                float angle = Vector3.Angle((targetPos - hostPos), gameObject.transform.forward);

                if (angle < Look_Offset)
                {
                    //if the player is looking at the pet
                    Pet.Looking();
                }
                else {
                    //if the player is looking at somewhere else
                    Pet.Wondering();
                }
            }
        }
    }
}
