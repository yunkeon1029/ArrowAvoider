using Godot;

// think of a way to notify health & max health without showing HealthManager
internal partial class Player : CharacterBody2D
{
    [Export]
    private HealthManager _healthManager;
    [Export]
    private HitReceiver _hitReciever;
    [Export]
    private PlayerMovement _playerMovement;

    public override void _PhysicsProcess(double elapsedTime)
    {
        Vector2 moveInput = new(0, 0);

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left)) 
            moveInput.X -= 1;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right)) 
            moveInput.X += 1;

        _playerMovement.ProcessMovement(moveInput, elapsedTime);
    }
}
