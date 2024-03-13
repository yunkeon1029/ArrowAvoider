using Godot;
using System;

public partial class Arrow : AnimatableBody2D, IDespawnable
{
	[Signal]
	public delegate void DespawningEventHandler();

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

	public void Despawn()
    {
        QueueFree();
		EmitSignal(SignalName.Despawning);
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not IDamageable damageable)
			return;

		damageable.Damage(_damage);
		QueueFree();
	}
}