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
	private PackedScene _settingsMenu;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		_startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
		_settingsButton.Pressed += OpenSettingsMenu;
		_exitButton.Pressed += () => GetTree().Quit();
    }

	private void OpenSettingsMenu()
	{
		var settingsMenu = _settingsMenu.Instantiate();

        settingsMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        settingsMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;

        AddChild(settingsMenu);
	}
}
