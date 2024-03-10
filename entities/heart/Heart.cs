using Godot;

public partial class Heart : Node2D, IDespawnable
{
    [Signal]
	public delegate void DespawningEventHandler();

	[Export]
	private Area2D _hitArea;
	[Export]
	private float _healing;

    public override void _Ready()
    {
		_hitArea.AreaEntered += OnHit;
		_hitArea.BodyEntered += OnHit;
    }

	public void Despawn()
    {
        QueueFree();
		EmitSignal(SignalName.Despawning);
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not IHealable healable)
			return;

		healable.Heal(_healing);
		QueueFree();
	}
}