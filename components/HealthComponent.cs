using Godot;

namespace ArrowAvoider;

[Tool]
public partial class HealthComponent : Node
{
    [Signal]
    public delegate void HealthChangedEventHandler(float delta);
    [Signal]
    public delegate void MaxHealthChangedEventHandler(float delta);
    [Signal]
    public delegate void HealthDepletedEventHandler();

    [Export]
    public float Health
    {
        get => _health;
        set => SetHealth(value);
    }

    [Export]
    public float MaxHealth
    {
        get => _maxHealth;
        set => SetMaxHealth(value);
    }

    [Export]
    private bool _startAtMaxHealth = true;

    private float _health = 0;
    private float _maxHealth = 0;

    public override void _Ready()
    {
        if (_startAtMaxHealth && !Engine.IsEditorHint())
            Health = MaxHealth;
    }

    public void SetHealth(float value)
    {
        float previousHealth = _health;

        _health = value;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (previousHealth != _health)
            EmitSignal(SignalName.HealthChanged, previousHealth - _health);

        if (_health == 0 && previousHealth != 0)
            EmitSignal(SignalName.HealthDepleted);
    }

    public void SetMaxHealth(float value)
    {
        float previousHealth = _health;
        float previousMaxHealth = _maxHealth;

        _maxHealth = value;
        _maxHealth = Mathf.Max(value, 0);

        _health = Mathf.Clamp(_health, 0, _maxHealth);

        if (previousHealth != _health)
            EmitSignal(SignalName.HealthChanged, previousHealth - _health);

        if (previousMaxHealth != _maxHealth)
            EmitSignal(SignalName.MaxHealthChanged, previousMaxHealth - _maxHealth);
            
        if (_health == 0 && previousHealth != 0)
            EmitSignal(SignalName.HealthDepleted);
    }
}