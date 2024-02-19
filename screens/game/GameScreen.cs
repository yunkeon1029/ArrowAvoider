using Godot;
using System;

namespace ArrowAvoider;

public partial class GameScreen : Node
{
	[Export]
	private Label _survivedTimeLabel;

	[Export(PropertyHint.File, "*.tscn")]
	private string _resultScreenPath;

	private float _survivedTime;

    public override void _PhysicsProcess(double delta)
    {
		_survivedTime += (float)delta;

		int minutes = Mathf.RoundToInt(_survivedTime / 60);
		int seconds =  Mathf.RoundToInt(_survivedTime - minutes);

		_survivedTimeLabel.Text = $"{minutes}:{seconds}";
    }

    public void OnPlayerDead()
	{
		GetTree().ChangeSceneToFile(_resultScreenPath);
	}
}
