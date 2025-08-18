using Godot;
using System;

internal partial class PauseMenu : CanvasLayer
{
    [Export]
    private BaseButton _continueButton;
    [Export]
    private BaseButton _settingsButton;
    [Export]
    private BaseButton _restartButton;
    [Export]
    private BaseButton _menuButton;

    [Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;
    [Export(PropertyHint.File, "*.tscn")]
    private string _mainMenuScenePath;

    [Export]
    private AudioEffect _audioEffect;
    [Export]
    private PackedScene _settingsMenu;
    [Export]
    private PackedScene _sweepUpTransition;
    [Export]
    private PackedScene _blackOutTransition;
    [Export]
    private PackedScene _volumeCrossfadeTransition;

    private int? _audioEffectIndex;

    public PauseMenu()
    {
        TreeEntered += AddAudioEffect;
        TreeExited += RemoveAudioEffect;
    }

    public override void _Ready()
    {
        SceneManager sceneManager = Singletons.GetInstance<SceneManager>();

        _continueButton.Pressed += QueueFree;
        _settingsButton.Pressed += OpenSettingsMenu;
        _menuButton.Pressed += ChangeSceneToMenu;

        _restartButton.Pressed += () => sceneManager.ChangeScene(_gameScenePath, _sweepUpTransition);

        _restartButton.Pressed += () => ProcessMode = ProcessModeEnum.Disabled;
        _menuButton.Pressed += () => ProcessMode = ProcessModeEnum.Disabled;
    }

    public override void _Process(double elapsedTime)
    {
        if (Input.IsActionJustPressed(ActionName.Escape))
            QueueFree();
    }

    private void OpenSettingsMenu()
    {
        var settingsMenu = _settingsMenu.Instantiate();

        settingsMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        settingsMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Always;

        settingsMenu.TreeEntered += () => Visible = false;
        settingsMenu.TreeExited += () => Visible = true;

        settingsMenu.TreeEntered += RemoveAudioEffect;
        settingsMenu.TreeExited += AddAudioEffect;

        AddSibling(settingsMenu);
    }

    private void AddAudioEffect()
    {
        int masterBusIndex = AudioServer.GetBusIndex(BusName.Master);

        AudioServer.AddBusEffect(masterBusIndex, _audioEffect);
        _audioEffectIndex = AudioServer.GetBusEffectCount(masterBusIndex) - 1;
    }

    private void RemoveAudioEffect()
    {
        if (_audioEffectIndex == null)
            return;

        int masterBusIndex = AudioServer.GetBusIndex(BusName.Master);

        AudioServer.RemoveBusEffect(masterBusIndex, _audioEffectIndex ?? -1);
        _audioEffectIndex = null;
    }

    private void ChangeSceneToMenu()
    {
        var sceneManager = Singletons.GetInstance<SceneManager>();
        var volumeCrossfade = _volumeCrossfadeTransition.Instantiate<TransitionAnimation>();

        Action<Node> sceneLoaded = _ => volumeCrossfade.PlayFadeOutAnimation(volumeCrossfade.QueueFree);

        volumeCrossfade.PlayFadeInAnimation();
        sceneManager.ChangeScene(_mainMenuScenePath, _blackOutTransition, sceneLoaded);
        
        sceneManager.AddChild(volumeCrossfade);
    }
}
