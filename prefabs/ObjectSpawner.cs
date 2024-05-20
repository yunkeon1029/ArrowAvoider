using Godot;

internal partial class ObjectSpawner : Node
{
	[Signal]
	public delegate void ObjectSpawnedEventHandler(Node spawnedObject);

	[Export] 
	private PackedScene _spawningObject;
	[Export] 
	private ReferenceRect _spawnArea;

	[Export]
	private float _spawnRate;
	[Export]
	private float _spawnRateIncreaceRate;
	[Export]
	private float _maxSpawnRate;

	private float _lastSpawnTime;

    public override void _PhysicsProcess(double elapsedTime)
    {
		_lastSpawnTime += (float)elapsedTime;
		_spawnRate += _spawnRateIncreaceRate * (float)elapsedTime;
		_spawnRate = Mathf.Min(_spawnRate, _maxSpawnRate);

		if (_lastSpawnTime >= 1.0 / _spawnRate)
		{
			SpawnObject();
			_lastSpawnTime = 0;
		}
    }

	protected virtual void SpawnObject()
	{
		Node obj = _spawningObject.Instantiate();
		Vector2 spawnPos = GetSpawnPos();

		if (obj is Node2D node2D)
			node2D.Position = spawnPos;

		if (obj is Control control)
			control.Position = spawnPos;

		AddChild(obj, true);
		EmitSignal(SignalName.ObjectSpawned, obj);
	}

	protected virtual Vector2 GetSpawnPos()
	{
		Vector2 minSpawnPos = _spawnArea.Position;
		Vector2 maxSpawnPos = _spawnArea.Position + _spawnArea.Size * _spawnArea.Scale;

		float x = (float)GD.RandRange(minSpawnPos.X, maxSpawnPos.X);
		float y = (float)GD.RandRange(minSpawnPos.Y, maxSpawnPos.Y);

		return new(x, y);
	}
}