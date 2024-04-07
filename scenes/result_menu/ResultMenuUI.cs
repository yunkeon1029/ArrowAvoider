using Godot;

internal partial class ResultMenuUI : CanvasLayer
{
	[Export]
	public BaseButton MenuButton { get; private set; }
	[Export]
	public BaseButton RetryButton { get; private set; }
	[Export]
	public Label ScoreLabel { get; private set; }

	public void UpdateScoreLabel(int score, int highScore)
	{
		ScoreLabel.Text = $"Score: {score} \n" +
						  $"High Score: {highScore}";
	}

	public void UpdateScoreLabel(int highScore)
	{
		ScoreLabel.Text = $"High Score: {highScore}";
	}
}
