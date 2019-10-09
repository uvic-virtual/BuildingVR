using System;
using UnityEngine;
using Health;

public class BasicMelee : MonoBehaviour
{
    [SerializeField] private int DamagePerHit = 25;

    private GameObject cachedEnemy;
    private IDamageable damage;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject != cachedEnemy)
        {
            cachedEnemy = col.gameObject;

            try
            {
                damage = cachedEnemy.transform.parent.GetComponentInChildren<IDamageable>();
            }
            catch (NullReferenceException)
            {
                damage = cachedEnemy.GetComponentInChildren<IDamageable>();
            }
        }

        if (damage != null)
        {
            damage.Hit(DamagePerHit);
            Debug.Log(damage.Health);
        }
    }
}
