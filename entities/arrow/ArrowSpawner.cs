using Godot;
using System;

namespace ArrowAvoider;

public partial class ArrowSpawner : Node
{
	[Export] 
	private Node _arrowContainer;
	[Export] 
	private PackedScene _arrowPrefab;

	[Export] 
	private Vector2 _minSpawnPos;
	[Export] 
	private Vector2 _maxSpawnPos;

	[Export] 
	private float _spawnRate;
	[Export] 
	private float _spawnRateIncreaceRate;

	private float _lastSpawnTime;

	public override void _PhysicsProcess(double delta)
	{
		_lastSpawnTime += (float)delta;
		_spawnRate += _spawnRateIncreaceRate * (float)delta;

		if (_lastSpawnTime >= 1.0 / _spawnRate)
		{
			SpawnArrow();
			_lastSpawnTime = 0;
		}
	}

	public void SpawnArrow()
	{
		Node2D arrow = _arrowPrefab.Instantiate<Node2D>();

		float x = (float)GD.RandRange(_minSpawnPos.X, _maxSpawnPos.X);
		float y = (float)GD.RandRange(_minSpawnPos.Y, _maxSpawnPos.Y);

		arrow.Position = new Vector2(x, y);
		_arrowContainer.AddChild(arrow);
	}
}