using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public HealthManager HealthManager { get; private set; }

    [Export] 
    private float _maxSpeed;
    [Export] 
    private float _accelerationRate;

    private Vector2 _velocity;

	public static Vector2 GetMoveInput()
	{
        Vector2 moveInput = new(0, 0);

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left)) 
            moveInput.X -= 1;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right)) 
            moveInput.X += 1;

        if (moveInput != Vector2.Zero)
            moveInput = moveInput.Normalized();

        return moveInput;
	}

    public override void _Ready()
    {
        PlayerInfo.UpdateHealth(HealthManager.Health);
        PlayerInfo.UpdateMaxHealth(HealthManager.MaxHealth);

        HealthManager.HealthChanged += _ => PlayerInfo.UpdateHealth(HealthManager.Health);
        HealthManager.MaxHealthChanged += _ => PlayerInfo.UpdateMaxHealth(HealthManager.MaxHealth);

        HealthManager.TreeExited += () => PlayerInfo.UpdateHealth(null);
        HealthManager.TreeExited += () => PlayerInfo.UpdateMaxHealth(null);
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
		Vector2 moveInput = GetMoveInput();
        Vector2 targetVelocity = moveInput * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        var collisionResult = MoveAndCollide(_velocity * (float)elapsedTime, false, 0);

        if (collisionResult != null)
            _velocity = Vector2.Zero;
    }
}
