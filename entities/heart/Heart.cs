using Godot;

public partial class Heart : Node2D
{
	[Export]
	public Area2D HitArea { get; private set; }

	[Export]
	private float _fallRate;
	[Export]
	private float _healing;

    public override void _Ready()
    {
		HitArea.AreaEntered += OnHit;
		HitArea.BodyEntered += OnHit;
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
		Position += Vector2.Down * _fallRate * (float)elapsedTime;
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject.IsInGroup("Player"))
			OnPlayerHit(hitObject);

		if (hitObject.IsInGroup("WorldBorder"))
			QueueFree();
	}

	private void OnPlayerHit(Node2D hitObject)
	{
		if (hitObject is not HitReceiver hitReceiver)
			return;

		hitReceiver.ApplyHealing(_healing);
		QueueFree();
	}
}