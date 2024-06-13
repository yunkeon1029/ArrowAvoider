using Godot;

internal partial class MainMenu : Node
{
	[Export]
	private BaseButton _startButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _exitButton;

	[Export]
	private PackedScene _settingsMenu;
	[Export]
	private PackedScene _creditsMenu;
	[Export]
	private PackedScene _blackOutTransition;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		_startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath, _blackOutTransition);
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
