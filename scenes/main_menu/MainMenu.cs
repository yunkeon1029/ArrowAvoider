using Godot;

internal partial class MainMenu : CanvasLayer
{
	[Export]
	private BaseButton _startButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _exitButton;
	[Export]
	private PackedScene _settingsWindow;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		_startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
		_settingsButton.Pressed += OpenSettingsWindow;
		_exitButton.Pressed += () => GetTree().Quit();
    }

	private void OpenSettingsWindow()
	{
		var settingsWindow = _settingsWindow.Instantiate<SettingsWindow>();

        settingsWindow.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        settingsWindow.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;

        AddChild(settingsWindow);
	}
}
