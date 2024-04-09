using Godot;
using System;

internal partial class SettingsWindow : CanvasLayer
{
	[Export]
	public BaseButton BackButton { get; private set; }

    public override void _Ready()
    {
		BackButton.Pressed += QueueFree;
    }
}
