using UnityEngine;

public class ZombieDelegates : MonoBehaviour
{
    public void KillZombie()
    {
        Destroy(transform.parent.gameObject);
    }
}
