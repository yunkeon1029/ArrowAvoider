using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class Hitbox : Area2D
{
	[Signal]
	public delegate void DamagedEventHandler(float damage);
    [Signal]
    public delegate void HealedEventHandler(float healing);

    [Export]
    public HealthManager HealthManager { get; private set; }

    public delegate void ModifyHealthEventHandler(ref float health);
    public delegate void ModifyDamageEventHandler(ref float health);

    public event ModifyHealthEventHandler ModifyHealth;
    public event ModifyDamageEventHandler ModifyDamage;

    public void ApplyDamage(float damage)
    {
        ModifyDamage?.Invoke(ref damage);
        HealthManager.Health -= damage;

        if (damage > 0)
            EmitSignal(SignalName.Damaged, damage);
    }

    public void ApplyHealing(float healing)
    {
        ModifyHealth?.Invoke(ref healing);
        HealthManager.Health += healing;

        if (healing > 0)
            EmitSignal(SignalName.Healed, healing);
    }
}