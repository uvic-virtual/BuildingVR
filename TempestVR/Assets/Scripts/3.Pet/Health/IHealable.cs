namespace Health
{
    /// <summary>
    /// Used to restore health.</summary>
    public interface IHealable
    {
        /// <summary>
        /// Current health of Entity.</summary>
        int Health { get; }
        
        /// <summary>
        /// Adds specified amount of health.</summary>
        /// <param name="health">Amount of health added.</param>
        void AddHealth(int health);
    }
}
