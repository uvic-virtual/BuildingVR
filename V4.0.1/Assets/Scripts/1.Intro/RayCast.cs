using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RayCast : MonoBehaviour {
    //Max distance for the raycast
    public float Default_Distance = 20.0f;

    public GameObject Collider;

    //Dot object in the end of the line
    //public GameObject Dot;
    //private Renderer DotRenderer;

    public VRInputModual Input;
    public LineRenderer Laser;

    private void Start()
    {
        //DotRenderer = Dot.GetComponent<MeshRenderer>();
        Laser = GetComponent<LineRenderer>();
        //Hide the laser
        Laser.enabled = false;
    }

    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //to hit canvans
        PointerEventData data = Input.GetData();
        float targetLength = data.pointerCurrentRaycast.distance == 0 ? Default_Distance : data.pointerCurrentRaycast.distance;


        //raycast
        RaycastHit hit = Create(targetLength);
        //default
        Vector3 endPosition;// = transform.position + (transform.forward * targetLength);
        //or base on hit
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == Collider)
            {
                endPosition = hit.point;
                Laser.enabled = true;
                Laser.SetPosition(0, transform.position);
                Laser.SetPosition(1, endPosition);
                //DotRenderer.enabled = true;

            }
            else
            {
                Laser.enabled = false;
                //DotRenderer.enabled = false;
            }
        }
        else {
            Laser.enabled = false;
        }
        //set position of the dot
        //Dot.transform.position = endPosition;
        //set linerenderer

    }
    private RaycastHit Create(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, length);
        return hit;
    }
}
