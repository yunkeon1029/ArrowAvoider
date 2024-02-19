using System;
using Godot;

namespace ArrowAvoider;

public partial class HealthHandler : Node
{
	[Signal]
	public delegate void HealthChangedEventHandler(float amount);
	[Signal]
	public delegate void HealthDepleatedEventHandler();

	[Export] 
	public float MaxHealth { get; private set; }
	[Export] 
	public float Health { get; private set; }

	[Export] 
	private bool _startAtFullHealth = true;

    public override void _Ready()
    {
		if (_startAtFullHealth)
			Health = MaxHealth;

		RequestReady();
    }

    public void ChangeHealth(float amount)
	{
		float previousHealth = Health;

		Health += amount;
		Health = Mathf.Clamp(Health, 0, MaxHealth);

		EmitSignal(SignalName.HealthChanged, Health - previousHealth);

		if (Health == 0)
			EmitSignal(SignalName.HealthDepleated);
	}
}