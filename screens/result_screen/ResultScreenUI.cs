using Godot;

namespace ArrowAvoider;

public partial class ResultScreenUI : CanvasLayer
{
	[Export]
	private Label _scoreLabel;
	[Export]
	private Label _highScoreLabel;

	public void NotifyScore(int score)
	{
		_scoreLabel.Text = $"{score} points";
	}
}
