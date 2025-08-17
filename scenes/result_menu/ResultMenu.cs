using Godot;
using System;

internal partial class ResultMenu : CanvasLayer
{
    [Export]
    private Label _scoreLabel;
    [Export]
    private BaseButton _menuButton;
    [Export]
    private BaseButton _retryButton;

    [Export]
    private PackedScene _blackOutTransition;
    [Export]
    private PackedScene _volumeCrossfadeTransition;
    
    [Export(PropertyHint.File, "*.tscn")]
    private string _mainMenuScenePath;
    [Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;

    private SceneManager _sceneManager;
    private SaveManager _saveManager;
    
    public override void _Ready()
    {
        _sceneManager = Singletons.GetInstance<SceneManager>();
        _saveManager = Singletons.GetInstance<SaveManager>();

        _retryButton.Pressed += () => _sceneManager.ChangeScene(_gameScenePath, _blackOutTransition);
        _menuButton.Pressed += ChangeSceneToMenu;
    }

    public void NotifyScore(int score)
    {
        int highScore = (int?)_saveManager.GetData(SaveName.HighScore) ?? 0;

        _scoreLabel.Text = $"Score: {score} \n" +
                           $"High Score: {highScore}";

        if (score > highScore)
        {
            _saveManager.SetData(SaveName.HighScore, score);
            _scoreLabel.Text = $"High Score: {score}";
        }
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