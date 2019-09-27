using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick : MonoBehaviour
{
    //Access function from the FadeIn.cs
    private FadeIn Fader;
    //Check if it's first time clicking the button
    private bool Clicked;
    //Sound effect
    public AudioSource SoundEffect;

    void Start()
    {
        Fader = GameObject.FindObjectOfType<FadeIn>();
        Clicked = false;
    }

    public void OnClickFunction()
    {
        //Do transition only once
        if (!Clicked)
        {
            SoundEffect.Play();
            Clicked = true;
            Fader.FadeBlack();
        }
    }
}
