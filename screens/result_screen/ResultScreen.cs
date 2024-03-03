using Godot;
using System;

namespace ArrowAvoider;

public partial class ResultScreen : Node
{
	[Export]
	private ResultScreenUI _resultScreenUI;

	public void NotifyScore(int score)
	{
		GD.Print(GetTree().CurrentScene.Name);
		_resultScreenUI.NotifyScore(score);
	}
}
