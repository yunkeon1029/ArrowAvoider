using Godot;
using System;

public partial class ArrowSpawner : ObjectSpawner
{
	[Export]
	private float _spawnRate;
	[Export]
	private float _spawnIncreaceRate;

	private float _lastSpawnedTime;

    public override void _Ready()
    {
		ObjectSpawned += _ => _lastSpawnedTime = 0;
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
		_spawnRate += _spawnIncreaceRate * (float)elapsedTime;

		if (_lastSpawnedTime < _spawnRate)
		{
			
		}
    }
}
