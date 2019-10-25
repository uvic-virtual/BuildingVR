using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Health
{
    public class HealthBarManager : MonoBehaviour
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
            float percentHealth = (float)myHealth.Health / myHealth.MaxHealth;
            healthBar.fillAmount = percentHealth;
        }
    }
}
