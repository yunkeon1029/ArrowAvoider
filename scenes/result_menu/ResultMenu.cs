using Godot;
using System;

internal partial class ResultMenu : Node
{
	private float _notifiedScore;

	public void NotifyScore(int score)
	{
		_notifiedScore = score;
		GD.Print(score);
	}
}
