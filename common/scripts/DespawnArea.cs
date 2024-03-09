using Godot;
using System;
using System.Linq;

public partial class DespawnArea : Area2D
{
	[Signal]
	public delegate void DespawnedNodeEventHandler(Node2D despawnedNode);

    public override void _Ready()
	{
		AreaEntered += OnDectection;
		BodyEntered += OnDectection;
	}

    private void OnDectection(Node2D dectectedNode)
	{
		if (dectectedNode is not IDespawnable despawnable)
			return;

		despawnable.Despawn();
		EmitSignal(SignalName.DespawnedNode, dectectedNode);
	}
}
