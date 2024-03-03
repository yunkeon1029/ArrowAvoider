using Godot;
using System;

namespace ArrowAvoider;

public partial class EffectReciever : Area2D
{
	[Signal]
	public delegate void EffectRecievedEventHandler(Effect effect);
	[Signal]
	public delegate void DamagedEventHandler(float amount);
	[Signal]
	public delegate void HealedEventHandler(float amount);

	public void ApplyEffect(Effect effect)
	{
		if (effect.Damage > 0)
			EmitSignal(SignalName.Damaged, effect.Damage);

		if (effect.HealAmount > 0)
			EmitSignal(SignalName.Healed, effect.HealAmount);

		EmitSignal(SignalName.EffectRecieved, effect);
	}
}
