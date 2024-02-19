using Godot;

namespace ArrowAvoider;

public partial class PlayerController : Node2D
{
    [Export] 
    private PlayerMovementHandler _playerMovement;

    // handle player health & UI

    public override void _PhysicsProcess(double elapsedTime)
    {
        _playerMovement.CalculateMovement(GetMoveInput(), elapsedTime);
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
