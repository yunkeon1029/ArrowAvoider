using Godot;
using System;

public partial class Gem : AnimatableBody2D
{
	[Export]
	public Area2D HitArea { get; private set; }

	[Export]
	private float _fallRate;
	[Export]
	private int _score;

	private ScoreManager _scoreManager;

    public override void _Ready()
    {
		HitArea.AreaEntered += OnHit;
		HitArea.BodyEntered += OnHit;

		_scoreManager = GlobalInstances.GetInstance<ScoreManager>();
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
		Position += Vector2.Down * _fallRate * (float)elapsedTime;
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not Player)
			return;

		_scoreManager.Score += _score;
		QueueFree();
	}
}

public partial class Gem : IDespawnable
{
    [Signal]
	public delegate void DespawningEventHandler();

	public void Despawn()
    {
        QueueFree();
		EmitSignal(SignalName.Despawning);
    }
}