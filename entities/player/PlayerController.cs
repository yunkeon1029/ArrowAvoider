using Godot;

namespace ArrowAvoider;

public partial class PlayerController : Node2D
{
	[Export]
	private HealthComponent _healthComponent;
	[Export]
	private HitboxComponent _hitboxComponent;
    [Export]
    private PlayerMovementHandler _movementHandler;
	[Export]
	private PlayerHealthRegenerator _healthRegenerator;
	[Export]
	private TextureProgressBar _healthBar;
    
	[Export(PropertyHint.File, "*.tscn")]
	private string _resultScreenPath;

    [Export]
	private float _invincibillityTime;

	private float? _lastDamagedTime = null;

	public override void _Ready()
	{
		SceneTree tree = GetTree();

		_healthComponent.HealthChanged += _ => UpdateHealthBar();
		_healthComponent.HealthDepleated += () => tree.ChangeSceneToFile(_resultScreenPath);

		_hitboxComponent.Damaged += RecieveDamage;
		_hitboxComponent.Healed += _healthComponent.ChangeHealth;

		UpdateHealthBar();
	}

	public override void _PhysicsProcess(double elapsedTime)
	{
        if (_lastDamagedTime != null)
		    _lastDamagedTime += (float)elapsedTime;

		_movementHandler.CalculateMovement(GetMoveInput(), elapsedTime);
		_healthRegenerator.CalculateHealthRegen(_lastDamagedTime, elapsedTime);
	}

	public void RecieveDamage(float amount)
	{
		if (_lastDamagedTime < _invincibillityTime)
			return;
		
		_lastDamagedTime = 0;
		_healthComponent.ChangeHealth(-amount);
	}

	public void UpdateHealthBar()
	{
		_healthBar.MaxValue = _healthComponent.MaxHealth;
		_healthBar.Value = _healthComponent.Health;
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
}