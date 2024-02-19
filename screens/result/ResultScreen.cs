using Godot;
using System;

namespace ArrowAvoider;

public partial class ResultScreen : Node
{
	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScreenPath;

    // for button pressed signal
    public void RestartGame()
	{
		GetTree().ChangeSceneToFile(_gameScreenPath);
	}
}