using Godot;

internal partial class MainMenu : Node
{
	[Export]
	public MainMenuUI MainMenuUI { get; private set; }

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		var startButton = MainMenuUI.StartButton;
		var exitButton = MainMenuUI.ExitButton;

		startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
		exitButton.Pressed += () => GetTree().Quit();
    }
}
