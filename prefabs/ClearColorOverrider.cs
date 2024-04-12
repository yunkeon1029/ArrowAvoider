using Godot;
using System;

internal partial class ClearColorOverrider : Node
{
	[Export]
	private Color _clearColor;

	public ClearColorOverrider()
	{
		Color defaultColor = RenderingServer.GetDefaultClearColor();

		TreeEntered += () => RenderingServer.SetDefaultClearColor(_clearColor);
		TreeExited += () => RenderingServer.SetDefaultClearColor(defaultColor);
	}
}
