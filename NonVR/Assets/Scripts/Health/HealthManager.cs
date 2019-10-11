using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used as a Health component on various gameobjects.</summary>
public class HealthManager : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private int MaxHealth = 100; //pretend this is a constant.

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
                Kill();
            }
        }
    }

    /// <summary>
    /// Delegate for Death event.</summary>
    public delegate void OnDeath();

    /// <summary>
    /// Raised when health reaches zero.</summary>
    public event OnDeath Death;

    /// <summary>
    /// Sets health to maxhealth.</summary>
    private void Start()
    {
        healthBar = GetComponentInChildren<Image>();
        Health = MaxHealth;
    }

    /// <summary>
    /// Calls Death() event.</summary>
    public void Kill()
    {
        Death?.Invoke();
    }
}