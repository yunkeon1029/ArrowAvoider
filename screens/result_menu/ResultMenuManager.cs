using Godot;
using System;

namespace ArrowAvoider;

public partial class ResultMenuManager : Node
{
	[Export]
	private ResultMenuUI _resultScreenUI;

	public void NotifyScore(int score)
	{
		GD.Print(GetTree().CurrentScene.Name);
		_resultScreenUI.NotifyScore(score);
	}
}
