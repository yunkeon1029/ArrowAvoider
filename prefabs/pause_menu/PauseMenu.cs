using Godot;
using System;

internal partial class PauseMenu : CanvasLayer
{
	[Signal]
	public delegate void RequestedRestartEventHandler();

	[Export]
	private BaseButton _continueButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _restartButton;

	[Export]
	private PackedScene _settingsMenu;

    public override void _Ready()
    {
		_continueButton.Pressed += QueueFree;
		_settingsButton.Pressed += OpenSettingsMenu;

		_restartButton.Pressed += () => EmitSignal(SignalName.RequestedRestart);
    }

    public override void _Process(double elapsedTime)
	{
		if (!Input.IsActionJustPressed(ActionName.Pause))
			return;

		QueueFree();
	}

	private void OpenSettingsMenu()
	{
		var settingsMenu = _settingsMenu.Instantiate();

        settingsMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        settingsMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Always;

        AddChild(settingsMenu);
	}
}
