using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class HUD : MonoBehaviour {
    public Image health;
    public Text text;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;
    public AudioSource oof;
    // Use this for initialization
    void Start () {
        health.fillAmount = 1;
        text.text = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (grabAction.GetStateDown(handType) && health.fillAmount >0)
        {
            //health.fillAmount -= 0.05f;
            //oof.Play();
        }
        if(health.fillAmount == 0)
        {
            text.text = "you're fucking dead";
        }
	}

   
}
