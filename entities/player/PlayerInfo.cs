using System;

public static class PlayerInfo
{
    public static event Action HealthUpdated;
    public static event Action MaxHealthUpdated;

    public static float? Health { get; private set; }
    public static float? MaxHealth { get; private set; }

    public static void UpdateHealth(float? health)
    {
        Health = health;
        HealthUpdated?.Invoke();
    }

    public static void UpdateMaxHealth(float? maxHealth)
    {
        MaxHealth = maxHealth;
        MaxHealthUpdated?.Invoke();
    }
}