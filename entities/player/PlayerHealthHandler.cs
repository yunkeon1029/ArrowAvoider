using Godot;

namespace ArrowAvoider;

public partial class PlayerHealthHandler : Node
{
    [Export]
    private HealthComponent _healthComponent;
    [Export]
    private HitboxComponent _hitboxComponent;
    [Export]
    private TextureProgressBar _healthBar;
    [Export]
    private float _healthRegenRate;
    [Export]
    private float _healthRegenDelay;
    [Export]
    private float _invincibilityTime;
    
    private float _lastDamagedTime;

    public override void _Ready()
    {
        _hitboxComponent.Damaged += OnDamaged;
        _healthComponent.HealthChanged += _ => UpdateHealthBar();

        UpdateHealthBar();
    }

    public void CalculateHealth(double elapsedTime)
    {
        _lastDamagedTime += (float)elapsedTime;

        if (_healthRegenDelay > _lastDamagedTime)
            _healthComponent.ChangeHealth(_healthRegenRate * (float)elapsedTime);
    }

    public void UpdateHealthBar()
    {
        _healthBar.MaxValue = _healthComponent.MaxHealth;
        _healthBar.Value = _healthComponent.Health;
    }

    public void OnDamaged(float amount)
    {
        if (_lastDamagedTime < _invincibilityTime)
            return;

        _lastDamagedTime = 0;
        _healthComponent.ChangeHealth(-amount);
    }
}