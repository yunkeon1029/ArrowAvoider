using Godot;

namespace ArrowAvoider;

public partial class PlayerMovementHandler : Node
{
    [Export]
    private Node2D _movingNode;
    [Export] 
    private PhysicsBody2D _collider;
    [Export] 
    private float _maxSpeed;
    [Export] 
    private float _acceleration;

    private Vector2 _velocity;

    public void CalculateMovement(Vector2 moveInput, double elapsedTime)
    {
        Vector2 targetVelocity = moveInput * _maxSpeed;
        _velocity = _velocity.MoveToward(targetVelocity, _acceleration);

        Vector2 targetMoveAmount = _velocity * (float)elapsedTime;
        KinematicCollision2D collisionResult = _collider?.MoveAndCollide(targetMoveAmount, true, 0);
        Vector2 moveAmount = collisionResult?.GetTravel() ?? targetMoveAmount;

        if (collisionResult != null)
            _velocity = Vector2.Zero;

        _movingNode.Position += moveAmount;
    }
}