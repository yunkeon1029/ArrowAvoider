using Godot;
using System;

namespace ArrowAvoider;

public partial class HitboxComponent : Area2D
{
	[Signal]
	public delegate void HitEventHandler(float healthDelta);
	[Signal]
	public delegate void DamagedEventHandler(float amount);
	[Signal]
	public delegate void HealedEventHandler(float amount);

	public void NotifyHit(float healthDelta)
	{
		if (healthDelta < 0)
			EmitSignal(SignalName.Damaged, -healthDelta);

		if (healthDelta > 0)
			EmitSignal(SignalName.Healed, healthDelta);

		EmitSignal(SignalName.Hit, healthDelta);	
	}
}