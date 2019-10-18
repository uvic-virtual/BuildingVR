using UnityEngine;

public class Bleah : MonoBehaviour
{
    public void PublicPrint()
    {
        Debug.Log("this is the public one");
    }

    private void PrivatePrint()
    {
        Debug.Log("this is the private one");
    }
}
