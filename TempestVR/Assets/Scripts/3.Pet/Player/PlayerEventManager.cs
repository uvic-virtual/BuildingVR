using Health;
using UnityEngine;

public class PlayerEventManager : MonoBehaviour
{
    private HealthManager health;

    void OnEnable()
    {
        health = GetComponentInChildren<HealthManager>();
        health.Death += OnPlayerDeath;
    }

    void OnDisable()
    {
        health.Death -= OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        Debug.Log("game over!");
    }
}
