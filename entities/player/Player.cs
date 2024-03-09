using System;
using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    public HealthManager HealthManager { get; private set; }

    [Export] 
    private float _maxSpeed;
    [Export] 
    private float _accelerationRate;

    [Export]
    private float _invincibilityTime;
    [Export]
    private Color _invincibillityMoudulate;

    private Vector2 _velocity;
    private bool _isInvincible;

    public Player()
    {
        TreeEntered += () => GlobalInstances.AddInstance(this);
        TreeExited += () => GlobalInstances.RemoveInstance(this);
    }

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

    public override void _PhysicsProcess(double elapsedTime)
    {
		Vector2 moveInput = GetMoveInput();
        Vector2 targetVelocity = moveInput * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        var collisionResult = MoveAndCollide(_velocity * (float)elapsedTime, false, 0);

        if (collisionResult != null)
            _velocity = Vector2.Zero;
    }

    private void StartInvincibillity()
    {
        SceneTree tree = GetTree();
        SceneTreeTimer invincibillityTimer = tree.CreateTimer(_invincibilityTime);

        Color previousMoudulate = Modulate;
        
        invincibillityTimer.Timeout += () => _isInvincible = false;
        invincibillityTimer.Timeout += () => Modulate = previousMoudulate;

        _isInvincible = true;
        Modulate = _invincibillityMoudulate;
    }
}

public partial class Player : IDamageable
{
    public void Damage(float damage)
    {
        if (_isInvincible == true)
            return;
        
        HealthManager.Health -= damage;
        StartInvincibillity();
    }
}