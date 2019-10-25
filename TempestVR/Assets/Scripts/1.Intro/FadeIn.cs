using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {
    //For the HUD animation
    public Animator Anime;
    public string newlevel;
    
    public void FadeBlack()
    {
        Anime.SetTrigger("Fade");
    }

    private void ChangeScene()
    {
        Debug.Log("asd");
        Application.LoadLevel(newlevel);
    }
}
