using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pet_UI : MonoBehaviour {
    public RawImage Face;
    public Texture Normal;
    public Texture Angry;
    public Texture Love;

    void Start()
    {
        Wondering();
    }

	public void Looking()
    {
        Face.texture = Love;
    }
    public void Wondering()
    {
        Face.texture = Normal;
    }
    public void Fighting()
    {
        Face.texture = Angry;
    }
}
