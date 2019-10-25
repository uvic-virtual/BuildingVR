using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//implement dynamic look_offset
public class LookAt : MonoBehaviour {
    private GameObject player;
    //length of ray cast
    public float Distance = 50;
    public float Look_Offset = 20;
    public float Distance_Offset = 0.7f;

    private Pet_UI UI;

    void Start()
    {
        player = GameObject.Find("VRCamera");
        UI = GameObject.FindObjectOfType<Pet_UI>();
    }

    void Update () {
        //the position the gameObject(camera) is at
        Vector3 playerPos = player.transform.position;
        Vector3 petPos = gameObject.transform.position;

        RaycastHit hitinfo;
        //When normalized, a vector keeps the same direction but its length is 1.0.
        //a vector that points at the pet from the host's position
        Vector3 ray_destination = (petPos - playerPos).normalized * Distance;

        //shot a ray from the camera to the pet
        Ray ray = new Ray(playerPos, ray_destination);

        //if hit anything
        if (Physics.Raycast(ray, out hitinfo, Distance))
        {
            //if hit the pet(with collider), which mean there's nothing blocking the filed of view
            if (hitinfo.collider.gameObject == gameObject)
            {
                //The angle in degrees between the vector |that's pointing the pet from the camera| and |the vector camera's facing|
                float angle = Vector3.Angle((petPos - playerPos), gameObject.transform.forward);

                if (angle < Look_Offset)
                {
                    //if the player is looking at the pet
                    UI.Looking();
                }
                else {
                    //if the player is looking at somewhere else
                    UI.Wondering();
                }
            }
        }
    }
}
