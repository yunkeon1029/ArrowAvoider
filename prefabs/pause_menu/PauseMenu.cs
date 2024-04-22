using Godot;
using System;

internal partial class PauseMenu : Node
{
	public override void _Process(double elapsedTime)
	{
		if (!Input.IsActionJustPressed("Pause"))
			return;

		QueueFree();
	}
}
