using Godot;
using System;

public partial class Gem : AnimatableBody2D, IDespawnable
{
    [Signal]
	public delegate void DespawningEventHandler();

	[Export]
	private Area2D _hitArea;
	[Export]
	private int _score;

	private ScoreManager _scoreManager;

    public override void _Ready()
    {
		_hitArea.AreaEntered += OnHit;
		_hitArea.BodyEntered += OnHit;

		_scoreManager = GlobalInstances.GetInstance<ScoreManager>();
    }

	public void Despawn()
    {
        QueueFree();
		EmitSignal(SignalName.Despawning);
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not Player)
			return;

		_scoreManager.Score += _score;
		QueueFree();
	}
}