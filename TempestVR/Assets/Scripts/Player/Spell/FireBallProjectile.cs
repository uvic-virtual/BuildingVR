using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallProjectile : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            TouchEnemy();
        }
    }
    
    private IEnumerable TouchEnemy()
    {
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.transform.DetachChildren();
        gameObject.transform.localScale *= 0;

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
