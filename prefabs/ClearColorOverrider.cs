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

		string defaultColorSettingsPath = "rendering/environment/defaults/default_clear_color";
		var defaultColor = (Color)ProjectSettings.GetSetting(defaultColorSettingsPath);

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
