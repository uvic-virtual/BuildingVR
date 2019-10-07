/// <summary>
/// Used to attack gameobjects.</summary>
public interface IDamageable
{
    /// <summary>
    /// Current health of entity.</summary>
    int Health { get; }

    /// <summary>
    /// Removes specified amount of health.
    /// </summary>
    /// <param name="damgeAmount">Amount of health to remove.</param>
    void Hit(int damgeAmount);

    /// <summary>
    /// Sets health to zero and calls Death event./// </summary>
    void Kill();
}
