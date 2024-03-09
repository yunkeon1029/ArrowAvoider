using Godot;
using System;

public partial class Arrow : StaticBody2D
{
	[Export]
	public Area2D HitArea { get; private set; }

	[Export]
	private float _fallRate;
	[Export]
	private float _damage;

    public override void _Ready()
	{
		HitArea.BodyEntered += OnHit;
		HitArea.AreaEntered += OnHit;
	}

    public override void _PhysicsProcess(double elapsedTime)
    {
		Position += Vector2.Down * _fallRate * (float)elapsedTime;
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not IDamageable damageable)
			return;

		damageable.Damage(_damage);
		QueueFree();
	}
}

public partial class Arrow : IDespawnable
{
    [Signal]
	public delegate void DespawningEventHandler();

	public void Despawn()
    {
        QueueFree();
		EmitSignal(SignalName.Despawning);
    }
}