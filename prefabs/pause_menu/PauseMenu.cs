using Godot;
using System;

internal partial class PauseMenu : CanvasLayer
{
	[Export]
	private BaseButton _continueButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _giveUpButton;
	[Export]
	private BaseButton _restartButton;
	[Export]
	private BaseButton _menuButton;

    [Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;
    [Export(PropertyHint.File, "*.tscn")]
	private string _mainMenuScenePath;
    [Export(PropertyHint.File, "*.tscn")]
	private string _resultMenuScenePath;
	
	[Export]
	private PackedScene _settingsMenu;


    public override void _Ready()
    {
		SceneManager sceneManager = Singletons.GetInstance<SceneManager>();

		_continueButton.Pressed += QueueFree;
		_settingsButton.Pressed += OpenSettingsMenu;

		_restartButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
		_menuButton.Pressed += () => sceneManager.ChangeScene(_mainMenuScenePath);
		_giveUpButton.Pressed += () => sceneManager.ChangeScene(_resultMenuScenePath);
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