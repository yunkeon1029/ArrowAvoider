using Godot;
using System;

internal partial class ResultMenuUI : CanvasLayer
{
	[Export]
	public BaseButton RestartButton { get; private set; }
	[Export]
	public Label ScoreLabel { get; private set; }

	public void UpdateScoreLabel(int score, int highScore)
	{
		ScoreLabel.Text = $"score: {score} \n" +
						  $"highScore: {highScore}";
	}
}