using Godot;

namespace ArrowAvoider;

public partial class PlayerController : Node2D
{
    [Export] 
    private PlayerMovementHandler _movementHandler;
    [Export]
    private PlayerHealthHandler _healthHandler;

    public override void _PhysicsProcess(double elapsedTime)
    {
        _movementHandler.CalculateMovement(GetMoveInput(), elapsedTime);
        _healthHandler.CalculateHealth(elapsedTime);
    }

    public static Vector2 GetMoveInput()
    {
        Vector2 moveInput = new(0, 0);

        if (Input.IsKeyPressed(Key.A) || Input.IsKeyPressed(Key.Left)) 
            moveInput.X -= 1;

        if (Input.IsKeyPressed(Key.D) || Input.IsKeyPressed(Key.Right)) 
            moveInput.X += 1;
            
        if (Input.IsKeyPressed(Key.Left)) moveInput.X -= 1;
        if (Input.IsKeyPressed(Key.Right)) moveInput.X += 1;

        if (moveInput != Vector2.Zero)
            moveInput = moveInput.Normalized();

        return moveInput;
    }
}
