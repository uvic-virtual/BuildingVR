using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// Used as a Health component on various gameobjects.</summary>
public class HealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int MaxHealth = 100; //pretend this is a constant.

    //Death event (stuff you want to happen when this zombie dies).
    [SerializeField] public UnityEvent Death;

    /// <summary>
    /// Health bar graphic that fills/shrinks depending on health.</summary>
    private Image healthBar;

    private int _health;
    /// <summary>
    /// Health of this entity. 
    /// The private set checks to see if it is a valid amount of health.</summary>
    public int Health
    {
        get { return _health; }

        set
        {
            _health = value;

            //sets filled amount to % of health remaining.
            if (healthBar != null)
            {
                healthBar.fillAmount = (float)_health / MaxHealth;
            }
            
            if (_health > MaxHealth)
            {
                _health = MaxHealth;
            }
            else if (_health <= 0)
            {
                Death?.Invoke();
            }
        }
    }

    /// <summary>
    /// Sets health to maxhealth.</summary>
    private void Start()
    {
        healthBar = GetComponentInChildren<Image>();
        Health = MaxHealth;
    }
}