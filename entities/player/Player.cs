using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    public HealthManager HealthManager { get; private set; }
    [Export]
    public PhysicsBody2D Collider { get; private set; }

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

    public override void _PhysicsProcess(double elapsedTime)
    {
		Vector2 moveInput = GetMoveInput();
        Vector2 targetVelocity = moveInput * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        Move(_velocity * (float)elapsedTime, () => _velocity = Vector2.Zero);
    }

    public void Move(Vector2 motion, Action onCollision)
    {
        KinematicCollision2D collisionResult = Collider?.MoveAndCollide(motion, true, 0);
        Vector2 moveAmount = collisionResult?.GetTravel() ?? motion;
        
        if (collisionResult != null)
            onCollision?.Invoke();

        Position += moveAmount;
    }
}