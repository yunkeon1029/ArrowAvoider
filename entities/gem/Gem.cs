using Godot;
using System;

public partial class Gem : Node2D
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
		if (hitObject.IsInGroup("Player"))
			OnPlayerHit();

		if (hitObject.IsInGroup("WorldBorder"))
			QueueFree();
	}

	private void OnPlayerHit()
	{
		_scoreManager.Score += _score;
		QueueFree();
	}
}
