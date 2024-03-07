using Godot;
using System;

public partial class HeartSpawner : ObjectSpawner
{
	private HealthManager playerHealthManager;

    public override void _Ready()
    {
		Player player = GlobalInstances.GetInstance<Player>();
		playerHealthManager = player.HealthManager;
    }

    protected override void SpawnObject()
    {
		float playerHealth = playerHealthManager.Health;
		float playerMaxHealth = playerHealthManager.MaxHealth;

		if (playerHealth < playerMaxHealth)
        	base.SpawnObject();
    }
}