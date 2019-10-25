using Health;
using UnityEngine;
using UnityEngine.UI;

public class ZombieSpellDamage : MonoBehaviour
{
    private Image healthBar;
    private HealthManager myHealth;

    private void Start()
    {
        myHealth = GetComponent<HealthManager>();
        healthBar = GetComponentInChildren<Image>();
    }

    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        float percentHealth = (float)(myHealth.Health / myHealth.MaxHealth);
        healthBar.fillAmount = percentHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light Attack")
        {
            LightDamage();
        }
    }

    private void LightDamage()
    {
        myHealth.Hit(25);
    }
}
