/// <summary>
/// Used to attack gameobjects.</summary>
public interface IDamageable
{
    int Health { get; set; }

    /// <summary>
    /// Sets health to zero and calls Death event./// </summary>
    void Kill();
}
