using Godot;

internal partial class MainMenu : Node
{
	[Export]
	private CanvasLayer _ui;
	[Export]
	private BaseButton _startButton;
	[Export]
	private BaseButton _settingsButton;
	[Export]
	private BaseButton _othersButton;

	[Export]
	private PackedScene _settingsMenu;
	[Export]
	private PackedScene _othersMenu;
	[Export]
	private PackedScene _blackOutTransition;

	[Export(PropertyHint.File, "*.tscn")]
	private string _gameScenePath;

    public override void _Ready()
    {
		var sceneManager = Singletons.GetInstance<SceneManager>();

		_startButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath, _blackOutTransition);
		_settingsButton.Pressed += OpenSettingsMenu;
		_othersButton.Pressed += OpenOthersMenu;
    }

	private void OpenSettingsMenu()
	{
		var settingsMenu = _settingsMenu.Instantiate();

		settingsMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
		settingsMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;

        AddChild(settingsMenu);
	}

	public void OpenOthersMenu()
	{
		var othersMenu = _othersMenu.Instantiate();

		othersMenu.TreeEntered += () => _ui.Visible = false;
		othersMenu.TreeExited += () => _ui.Visible = true;

		AddChild(othersMenu);
	}
}
