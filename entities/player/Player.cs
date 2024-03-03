using Godot;

namespace ArrowAvoider;

public partial class Player : Node2D
{
	[Export]
	public HealthComponent HealthComponent { get; private set; }
	[Export]
	public EffectReciever EffectReciever { get; private set; }
	[Export] 
    public PhysicsBody2D Collider { get; private set; }

	[Export] 
    private float _maxSpeed;
    [Export] 
    private float _acceleration;
    [Export]
	private float _invincibillityTime;

	private Vector2 _velocity;
	private float? _lastDamagedTime = null;

	public override void _Ready()
	{
		SceneTree tree = GetTree();

		EffectReciever.Damaged += RecieveDamage;
		EffectReciever.Healed += amount => HealthComponent.Health += amount;
	}
	
    public override void _PhysicsProcess(double elapsedTime)
	{
        if (_lastDamagedTime != null)
		    _lastDamagedTime += (float)elapsedTime;

		CalculateMovement(elapsedTime);
	}
	
	public Vector2 GetMoveInput()
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

    public void CalculateMovement(double elapsedTime)
    {
		Vector2 moveInput = GetMoveInput();
        Vector2 targetVelocity = moveInput * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _acceleration);

        Vector2 targetMoveAmount = _velocity * (float)elapsedTime;
        KinematicCollision2D collisionResult = Collider?.MoveAndCollide(targetMoveAmount, true, 0);
        Vector2 moveAmount = collisionResult?.GetTravel() ?? targetMoveAmount;

        if (collisionResult != null)
            _velocity = Vector2.Zero;

        Position += moveAmount;
    }

	public void RecieveDamage(float amount)
	{
		if (_lastDamagedTime < _invincibillityTime)
			return;
		
		_lastDamagedTime = 0;
		HealthComponent.Health -= amount;
	}
}