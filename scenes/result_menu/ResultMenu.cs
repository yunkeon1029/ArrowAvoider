using Godot;

internal partial class ResultMenu : CanvasLayer
{
	[Export]
	private Label _scoreLabel;
	[Export]
	private BaseButton _menuButton;
	[Export]
	private BaseButton _retryButton;
	
	[Export(PropertyHint.File, "*.tscn")]
    private string _mainMenuScenePath;
	[Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;

	private SceneManager _sceneManager;
	private SaveManager _saveManager;
	
	public override void _Ready()
    {
		_sceneManager = Singletons.GetInstance<SceneManager>();
		_saveManager = Singletons.GetInstance<SaveManager>();

		_menuButton.Pressed += () => _sceneManager.ChangeScene(_gameScenePath);
		_retryButton.Pressed += () => _sceneManager.ChangeScene(_mainMenuScenePath);
    }

	public void NotifyScore(int score)
	{
		int highScore = (int?)_saveManager.GetData("HighScore") ?? 0;

		_scoreLabel.Text = $"Score: {score} \n" +
						   $"High Score: {highScore}";

		if (score > highScore)
		{
			_saveManager.SetData("HighScore", score);
			_scoreLabel.Text = $"High Score: {score}";
		}
	}
}