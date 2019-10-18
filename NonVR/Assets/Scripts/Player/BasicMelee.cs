using System;
using UnityEngine;

public class BasicMelee : MonoBehaviour
{
    [SerializeField] private int DamagePerHit = 50;

    private IDamageable CachedDamage;

    private GameObject CachedGameObject;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(CachedGameObject))
        {
            CachedGameObject = other.gameObject;

            try
            {
                CachedDamage = CachedGameObject.transform.parent.GetComponentInChildren<IDamageable>();
            }
            catch (NullReferenceException)
            {
                CachedDamage = CachedGameObject.GetComponentInChildren<IDamageable>();
            }
        }

        if (CachedDamage != null)
        {
            CachedDamage.Health -= DamagePerHit;
            Debug.Log(CachedDamage.Health);
        }
    }
}
