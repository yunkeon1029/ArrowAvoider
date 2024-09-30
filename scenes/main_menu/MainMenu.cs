using Godot;
using System;

internal partial class MainMenu : Node
{
	[Export]
	private BaseButton _startButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _exitButton;

	[Export]
	private PackedScene _settingsMenu;
	[Export]
	private PackedScene _blackOutTransition;
	[Export]
	private PackedScene _volumeCrossfadeTransition;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

	public override void _Ready()
	{
		var sceneManager = Singletons.GetInstance<SceneManager>();

		_startButton.Pressed += ChangeSceneToGame;
		_settingsButton.Pressed += OpenSettingsMenu;
		_exitButton.Pressed += () => GetTree().Quit();
	}

	private void OpenSettingsMenu()
	{
		var settingsMenu = _settingsMenu.Instantiate();

		settingsMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
		settingsMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;

		AddChild(settingsMenu);
	}

	private void ChangeSceneToGame()
	{
		var sceneManager = Singletons.GetInstance<SceneManager>();
		var volumeCrossfade = _volumeCrossfadeTransition.Instantiate<TransitionAnimation>();

		Action<Node> sceneLoaded = _ => volumeCrossfade.PlayFadeOutAnimation(volumeCrossfade.QueueFree);

		volumeCrossfade.PlayFadeInAnimation();
		sceneManager.ChangeScene(_gameScenePath, _blackOutTransition, sceneLoaded);
		
		sceneManager.AddChild(volumeCrossfade);
	}
}
