using Godot;
using System;

internal partial class OthersMenu : CanvasLayer
{
	[Export]
	private BaseButton _returnButton;

    public override void _Ready()
    {
		_returnButton.Pressed += QueueFree;
    }

    public override void _Process(double delta)
    {
		if (Input.IsActionJustPressed(ActionName.Escape))
			QueueFree();
    }
}
