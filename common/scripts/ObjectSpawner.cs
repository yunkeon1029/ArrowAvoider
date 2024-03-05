using Godot;

public partial class ObjectSpawner : Node
{
	[Signal]
	public delegate void ObjectSpawnedEventHandler(Node spawnedObject);

	[Export] 
	private PackedScene _spawningObject;
	[Export] 
	private ReferenceRect _spawnArea;

	public void SpawnObject()
	{
		Node obj = _spawningObject.Instantiate();

		Vector2 minSpawnPos = _spawnArea.Position;
		Vector2 maxSpawnPos = _spawnArea.Position + _spawnArea.Size * _spawnArea.Scale;

		float x = (float)GD.RandRange(minSpawnPos.X, maxSpawnPos.X);
		float y = (float)GD.RandRange(minSpawnPos.Y, maxSpawnPos.Y);

		if (obj is Node2D node2D)
			node2D.Position = new(x, y);

		if (obj is Control control)
			control.Position = new(x, y);

		AddChild(obj);
		EmitSignal(SignalName.ObjectSpawned, obj);
	}
}