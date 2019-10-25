using UnityEngine;

namespace Health
{
    /// <summary>
    /// Used as a Health component on various gameobjects.</summary>
    public class HealthManager : MonoBehaviour, IDamageable, IHealable
    {
        [SerializeField] private int _maxHealth = 100; //pretend this is a constant.
        public int MaxHealth { get { return _maxHealth; } }
        
        /// <summary>
        /// Private health total.
        /// What Health property sets/gets.</summary>
        private int _health;

        /// <summary>
        /// Health of this entity. 
        /// The private set checks to see if it is a valid amount of health.</summary>
        public int Health
        {
            get { return _health; }

            private set
            {
                _health = value;

                if (_health > _maxHealth)
                {
                    _health = _maxHealth;
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
        void Start()
        {
            Health = _maxHealth;
        }
        
        /// <summary>
        /// Removes the specified amount of health.
        /// </summary>
        /// <param name="damgeAmount">Amount of health to remove.</param>
        public void Hit(int damgeAmount)
        {
            Health -= damgeAmount;
        }

        /// <summary>
        /// Adds the specified amount of health.
        /// </summary>
        /// <param name="healAmount">Amount of health to add.</param>
        public void AddHealth(int healAmount)
        {
            Health += healAmount;
        }

        /// <summary>
        /// Calls Death() event.</summary>
        public void Kill()
        {
            if (Death != null)
            {
                Death();
            }
        }
    }
}
