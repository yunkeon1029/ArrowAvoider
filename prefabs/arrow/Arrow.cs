using Godot;
using System;

internal partial class Arrow : AnimatableBody2D, IDespawnable
{
	[Export]
	private Area2D _hitArea;
	[Export]
	private float _damage;

    public override void _Ready()
	{
		_hitArea.BodyEntered += OnHit;
		_hitArea.AreaEntered += OnHit;

		RequestReady();
	}
	
    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not Player player)
			return;

		player.ApplyDamage(_damage);
		QueueFree();
	}
}