using Godot;
using System;

public partial class ArrowSpawner : ObjectSpawner
{
	[Export]
	private float _spawnRate;
	[Export]
	private float _spawnRateIncreaceRate;

	private float _lastSpawnTime;

    public override void _PhysicsProcess(double elapsedTime)
    {
		_lastSpawnTime += (float)elapsedTime;
		_spawnRate += _spawnRateIncreaceRate * (float)elapsedTime;

		if (_lastSpawnTime >= 1.0 / _spawnRate)
		{
			SpawnObject();
			_lastSpawnTime = 0;
		}
    }
}
