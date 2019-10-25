using UnityEngine;
using Health;

/// <summary>
/// Contains Delegates added to this zombie's HealthManager's Death event.</summary>
public class ZombieEventManager : MonoBehaviour {

    private HealthManager health;

	void OnEnable ()
    {
        health = GetComponent<HealthManager>();
        health.Death += OnZombieDeath;
	}
    
    void OnDestroy()
    {
        health.Death -= OnZombieDeath;
    }

    /// <summary>
    /// Destroys zombie.</summary>
    private void OnZombieDeath()
    {
        Destroy(gameObject);
    }
}
