using Godot;
using System;

namespace ArrowAvoider;

public partial class Arrow : Node2D
{
	[Export] 
	private float _fallSpeed;
	[Export] 
	private float _damage;

    public override void _PhysicsProcess(double delta)
    {
		Position += Vector2.Down * _fallSpeed * (float)delta;
    }

    public void OnAreaEntered(Area2D area)
	{
		if (area is EffectReciever effectReciever)
		{
            Effect effect = new()
            {
                Damage = _damage,
				HealAmount = 0
            };

            effectReciever.ApplyEffect(effect);
			QueueFree();
		}

		if (area.IsInGroup("DespawnArea"))
			QueueFree();
	}
}
