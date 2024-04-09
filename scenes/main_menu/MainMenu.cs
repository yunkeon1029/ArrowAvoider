using Godot;

internal partial class MainMenu : Node
{
	[Export]
	public MainMenuUI MainMenuUI { get; private set; }
	[Export]
	private PackedScene _settingsWindow;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		var startButton = MainMenuUI.StartButton;
		var settingsButton = MainMenuUI.SettingsButton;
		var exitButton = MainMenuUI.ExitButton;

		startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
		settingsButton.Pressed += OpenSettingsWindow;
		exitButton.Pressed += () => GetTree().Quit();
    }

	private void OpenSettingsWindow()
	{
		var settingsWindow = _settingsWindow.Instantiate<SettingsWindow>();

        settingsWindow.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        settingsWindow.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;

        AddChild(settingsWindow);
	}
}
