using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Used as a Health component on various gameobjects.</summary>
public class HealthManager : MonoBehaviour, IDamageable
{
    [SerializeField] private int MaxHealth = 100; //pretend this is a constant.

    //Death event (stuff you want to happen when this zombie dies).
    [SerializeField] private UnityEvent Death;

    /// <summary>
    /// Health bar graphic that fills/shrinks depending on health.</summary>
    [SerializeField] private Image HealthBar;

    private int _health;
    /// <summary>
    /// Health of this entity. 
    /// The private set checks to see if it is a valid amount of health.</summary>
    public int Health
    {
        get { return _health; }

        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            
            if (HealthBar != null)
            {
                HealthBar.fillAmount = (float)_health / MaxHealth;
            }

            if (_health == 0)
            {
                Death?.Invoke();
            }
        }
    }

    /// <summary>
    /// Sets health to maxhealth.</summary>
    private void Start()
    {
        if (HealthBar == null)
        {
            HealthBar = GetComponentInChildren<Image>();
        }
        Health = MaxHealth;
    }
}