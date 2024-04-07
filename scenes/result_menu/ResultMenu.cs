using Godot;

internal partial class ResultMenu : Node
{
	[Export]
	public ResultMenuUI ResultMenuUI { get; private set; }
	
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

		var retryButton = ResultMenuUI.RetryButton;
		var menuButton = ResultMenuUI.MenuButton;

		retryButton.Pressed += () => _sceneManager.ChangeScene(_gameScenePath);
		menuButton.Pressed += () => _sceneManager.ChangeScene(_mainMenuScenePath);
    }

	public void NotifyScore(int score)
	{
		int highScore = (int?)_saveManager.GetData("HighScore") ?? 0;

		if (score <= highScore)
			ResultMenuUI.UpdateScoreLabel(score, highScore);

		if (score > highScore)
		{
			_saveManager.SetData("HighScore", score);
			ResultMenuUI.UpdateScoreLabel(score);
		}
	}
}