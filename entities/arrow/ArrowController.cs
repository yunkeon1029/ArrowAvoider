using Godot;
using System;

namespace ArrowAvoider;

public partial class ArrowController : Node2D
{
	[Export] 
	private float _speed;
	[Export] 
	private float _damage;

    public override void _PhysicsProcess(double delta)
    {
		Position += Vector2.Down * _speed * (float)delta;
    }

    public void OnAreaEntered(Area2D area)
	{
		if (area is HitboxComponent hitbox)
		{
			hitbox.NotifyHit(-_damage);
			QueueFree();
		}

		if (area.IsInGroup("DespawnArea"))
			QueueFree();
	}
}
