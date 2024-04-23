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
    public PlayerMovement PlayerMovement { get; private set; }

    [Export]
    private AudioStream _hitSound;
    [Export]
    private AudioStream _hitBlockedSound;

    [Export]
    private float _invincibilityTime;
    [Export]
    private Color _invincibilityModulate;

    private float _leftInvincibilityTime;

    public override void _PhysicsProcess(double elapsedTime)
    {
        CalculateMovement(elapsedTime);
        CalculateInvincibility(elapsedTime);
    }

    public void ApplyHit(float damage)
    {
        CalculateHitSound();
        CalculateHitDamage(damage);
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

    private void CalculateMovement(double elapsedTime)
    {
        Vector2 moveInput = new();

        if (Input.IsActionPressed(ActionName.MoveLeft))
            moveInput += Vector2.Left;

        if (Input.IsActionPressed(ActionName.MoveRight))
            moveInput += Vector2.Right;

        PlayerMovement.CalculateMovement(moveInput, elapsedTime);
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
        if (_invincibilityTime <= 0)
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
}