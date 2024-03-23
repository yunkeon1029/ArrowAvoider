using Godot;
using System;

internal partial class Gem : AnimatableBody2D, IDespawnable
{
	[Export]
	private Area2D _hitArea;
	[Export]
	private int _score;

	private ScoreManager _scoreManager;

    public override void _Ready()
    {
		Game game = Singletons.GetInstance<Game>();

		_hitArea.AreaEntered += OnHit;
		_hitArea.BodyEntered += OnHit;

		_scoreManager = game.ScoreManager;

		RequestReady();
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not Player)
			return;

		_scoreManager.Score += _score;
		QueueFree();
	}
}