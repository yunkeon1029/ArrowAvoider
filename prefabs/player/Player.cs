using Godot;

internal partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void InvincibilityStartedEventHandler();
    [Signal]
    public delegate void InvincibilityEndedEventHandler();
    
    [Export]
    public HealthManager HealthManager { get; private set; }
    [Export]
    public StaticBody2D Collider { get; private set; }

    [Export]
    private AnimationPlayer _animationPlayer;
    [Export]
    private Sprite2D _sprite;

    [Export]
    private AudioStream _hitSound;
    [Export]
    private AudioStream _hitBlockedSound;

    [Export]
    private Texture2D _playerRightTexture;
    [Export]
    private Texture2D _playerLeftTexture;

    [Export]
    private string _fadeOutAnimationName;

    [Export]
	private float _maxSpeed;
	[Export]
	private float _accelerationRate;

    [Export]
    private float _invincibilityTime;
    [Export]
    private Color _invincibilityModulate;

    private Vector2 _velocity;
    private float _leftInvincibilityTime;

    public override void _Ready()
    {
        HealthManager.HealthDepleted += () => CollisionLayer = 0;
        HealthManager.HealthDepleted += () => CollisionMask = 0;
        HealthManager.HealthDepleted += () => _animationPlayer.Play(_fadeOutAnimationName);
    }

    public override void _PhysicsProcess(double elapsedTime)
    {
        CalculateMovement(elapsedTime);
        CalculateInvincibility(elapsedTime);
        UpdateLookDir();
    }

    public void ApplyHit(float damage)
    {
        CalculateHitSound();
        CalculateHitDamage(damage);
    }

    private Vector2 GetMoveInput()
    {
        Vector2 moveInput = new();

        if (Input.IsActionPressed(ActionName.MoveLeft))
            moveInput += Vector2.Left;

        if (Input.IsActionPressed(ActionName.MoveRight))
            moveInput += Vector2.Right;

        return moveInput;
    }

    private void CalculateHitSound()
    {
        AudioManager audioManager = Singletons.GetInstance<AudioManager>();

        if (_leftInvincibilityTime > 0)
            audioManager.PlaySFX(_hitBlockedSound);

        if (_leftInvincibilityTime <= 0)
            audioManager.PlaySFX(_hitSound);
    }

    private void CalculateHitDamage(float damage)
    {
        if (_leftInvincibilityTime > 0 || damage <= 0)
                return;

        HealthManager.Health -= damage;
        StartInvincibility();
    }

	public void CalculateMovement(double elapsedTime)
	{
        Vector2 moveInput = GetMoveInput();
        Vector2 targetVelocity = Vector2.Zero;

		if (moveInput != Vector2.Zero)
			targetVelocity = moveInput.Normalized() * _maxSpeed;

        _velocity = _velocity.MoveToward(targetVelocity, _accelerationRate * (float)elapsedTime);

        var moveAmount = _velocity * (float)elapsedTime;
        var collisionResult = Collider.MoveAndCollide(moveAmount, true, 0);

        Translate(collisionResult?.GetTravel() ?? moveAmount);

        if (collisionResult != null)
            _velocity = Vector2.Zero;
	}

    private void CalculateInvincibility(double elapsedTime)
    {
        if (_leftInvincibilityTime <= 0)
            return;

        _leftInvincibilityTime -= (float)elapsedTime;
        _leftInvincibilityTime = Mathf.Max(0, _leftInvincibilityTime);

        if (_leftInvincibilityTime <= 0)
            EndInvincibility();
    }

    private void StartInvincibility()
    {
        if (_invincibilityTime <= 0 || HealthManager.Health == 0)
            return;

        _leftInvincibilityTime = _invincibilityTime;
        Modulate = _invincibilityModulate;

        EmitSignal(SignalName.InvincibilityStarted);
    }

    private void EndInvincibility()
    {
        Modulate = new Color(1, 1, 1, 1);
        EmitSignal(SignalName.InvincibilityEnded);
    }

    private void UpdateLookDir()
    {
        Vector2 moveInput = GetMoveInput();

        if (moveInput == Vector2.Right)
            _sprite.Texture = _playerRightTexture;

        if (moveInput == Vector2.Left)
            _sprite.Texture = _playerLeftTexture;
    }
}