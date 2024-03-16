using Godot;

[Tool]
internal partial class HealthManager : Node
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

    private float _health;
    private float _maxHealth;

    public override void _EnterTree()
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
            EmitSignal(SignalName.HealthChanged, _health - previousHealth);

        if (_health == 0 && previousHealth != 0)
            EmitSignal(SignalName.HealthDepleted);
    }

    public void SetMaxHealth(float value)
    {
        float previousMaxHealth = _maxHealth;

        _maxHealth = value;
        _maxHealth = Mathf.Max(value, 0);

        Health = Mathf.Clamp(_health, 0, _maxHealth);

        if (previousMaxHealth != _maxHealth)
            EmitSignal(SignalName.MaxHealthChanged, _maxHealth - previousMaxHealth);
    }
}