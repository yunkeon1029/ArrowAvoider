using Godot;
using System;

public partial class FallingObject : Node
{
	[Export]
	private CanvasItem _movingObject;
	[Export]
	private float _fallRate;

    public override void _PhysicsProcess(double elapsedTime)
    {
		Vector2 moveAmount = Vector2.Down * _fallRate * (float)elapsedTime;

		if (_movingObject is Node2D node2D)
			node2D.Position += moveAmount;
		
		if (_movingObject is Control control)
			control.Position += moveAmount;
    }
}