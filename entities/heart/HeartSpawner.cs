using Godot;
using System;

public partial class HeartSpawner : ObjectSpawner
{
	[Export]
	private float _spawnRate;

	private float _lastSpawnTime;

    public override void _Process(double elapsedTime)
    {
		_lastSpawnTime += (float)elapsedTime;

		float? playerHealth = PlayerInfo.Health;
		float? playerMaxHealth = PlayerInfo.MaxHealth;

		if (_lastSpawnTime >= 1.0f / _spawnRate && playerHealth < playerMaxHealth)
		{
			_lastSpawnTime = 0;
			SpawnObject();
		}
    }
}