using Godot;
using System;

internal partial class ResultMenu : Node
{
	[Export]
	public ResultMenuUI ResultMenuUI { get; private set; }
	
	[Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;
	
	public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();
		var restartButton = ResultMenuUI.RestartButton;

		restartButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);
    }

	public int GetHighScore()
	{
		return 0;
		throw new NotImplementedException();
	}

	public void SetHighScore(int value)
	{
		return;
		throw new NotImplementedException();
	}

	public void NotifyScore(int score)
	{
		int highScore = GetHighScore();

		if (score <= highScore)
			ResultMenuUI.UpdateScoreLabel(score, highScore);

		if (score > highScore)
		{
			SetHighScore(score);
			ResultMenuUI.UpdateScoreLabel(score);
		}
	}
}