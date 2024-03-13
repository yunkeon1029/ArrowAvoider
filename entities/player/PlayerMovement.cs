using Godot;

public partial class PlayerMovement : Node
{
	[Export]
	private Player _player;

	[Export]
	private float _maxSpeed;
	[Export]
	private float _accelerationRate;

	private Vector2 _velocity;

	public void ProcessMovement(Vector2 moveInput, double elapsedTime)
	{
        Vector2 targetVelocity = Vector2.Zero;

		if (moveInput != Vector2.Zero)
			targetVelocity = moveInput.Normalized() * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        var collisionResult = _player.MoveAndCollide(_velocity * (float)elapsedTime, false, 0);

        if (collisionResult != null)
            _velocity = Vector2.Zero;
	}
}