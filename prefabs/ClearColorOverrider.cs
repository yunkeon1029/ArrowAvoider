using Godot;
using System;

[Tool]
internal partial class ClearColorOverrider : Node
{
	[Export]
	public Color ClearColor
	{
		get => _clearColor;
		set => SetClearColor(value);
	}

	private Color _clearColor;

	public ClearColorOverrider()
	{
		if (Engine.IsEditorHint())
			return;

		Color defaultColor = RenderingServer.GetDefaultClearColor();

		TreeEntered += () => RenderingServer.SetDefaultClearColor(_clearColor);
		TreeExited += () => RenderingServer.SetDefaultClearColor(defaultColor);
	}

    public void SetClearColor(Color value)
	{
		if (!Engine.IsEditorHint())
			RenderingServer.SetDefaultClearColor(value);

		_clearColor = value;
	}
}
