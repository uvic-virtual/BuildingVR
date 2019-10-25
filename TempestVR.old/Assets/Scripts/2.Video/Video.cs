using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using System;
using System.Threading;


public class Video : MonoBehaviour
{
    public AudioSource audioSource;
    public RawImage rawImage;
    public string newlevel;
    public VideoPlayer videoPlayer;
    private bool skip = false;

    // Use this for initialization
    void Start()
    {
        rawImage.color = Color.black;
        StartCoroutine(PlayVideo());
    }

    void Update()
    {
        if (skip && !videoPlayer.isPlaying)
        {
            Application.LoadLevel(newlevel);
        }
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();
        //yield so code after will still run
        yield return new WaitForSeconds(2);
        //color for the video
        rawImage.texture = videoPlayer.texture;
        rawImage.color = Color.white;
        videoPlayer.Play();
        audioSource.Play();
        skip = true;
    }
}
