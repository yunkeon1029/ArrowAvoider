using Godot;
using System;

internal partial class ResultMenu : Node
{
	[Export]
	public ResultMenuUI ResultMenuUI { get; private set; }

	[Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;

	private int _notifiedScore;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();
		var restartButton = ResultMenuUI.RestartButton;

		restartButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath);

		ResultMenuUI.UpdateScoreLabel(_notifiedScore, -1);
    }

    public void NotifyScore(int score)
	{
		_notifiedScore = score;
	}
}
