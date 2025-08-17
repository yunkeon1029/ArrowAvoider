using Godot;

internal partial class Arrow : AnimatableBody2D
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

        player.ApplyHit(_damage);
        QueueFree();
    }
}