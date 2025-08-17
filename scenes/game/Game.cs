using Godot;
using System;
using System.Linq;

internal partial class Game : Node, ISingleton
{
    [Export]
    public Player Player { get; private set; }
    [Export]
    public ScoreManager ScoreManager { get; private set; }

    [Export]
    private HeartContainer _heartContainer;
    [Export]
    private Label _scoreLabel;

    [Export]
    private PackedScene _pauseMenu;
    [Export]
    private PackedScene _sweepUpTransition;

    [Export(PropertyHint.File, "*.tscn")]
    private string _resultMenuScenePath;

    public Game()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);
    }

    public override void _Ready()
    {
        var sceneManager = Singletons.GetInstance<SceneManager>();
        var healthManager = Player.HealthManager;

        healthManager.HealthChanged += _ => UpdateHeartContainer();
        healthManager.MaxHealthChanged += _ => UpdateHeartContainer();
        healthManager.HealthDepleted += () => sceneManager.ChangeScene(_resultMenuScenePath, _sweepUpTransition, OnSceneLoaded);

        ScoreManager.ScoreChanged += _ => UpdateScoreLabel(ScoreManager.Score);

        UpdateHeartContainer();
        UpdateScoreLabel(ScoreManager.Score);

        RequestReady();
    }

    public override void _Process(double elapsedTime)
    {
        if (Input.IsActionJustPressed(ActionName.Escape))
            OpenPauseMenu();
    }

    private void UpdateHeartContainer()
    {
        var healthManager = Player.HealthManager;

        float maxHealth = healthManager.MaxHealth;
        float health = healthManager.Health;

        _heartContainer.Update(maxHealth, health);
    }

    private void UpdateScoreLabel(float score)
    {
        _scoreLabel.Text = $"score: {score}";
    }

    private void OnSceneLoaded(Node loadedScene)
    {
        if (loadedScene is not ResultMenu resultMenu)
            return;

        int score = ScoreManager.Score;
        resultMenu.Ready += () => resultMenu.NotifyScore(score);
    }

    private void OpenPauseMenu()
    {
        var pauseMenu = _pauseMenu.Instantiate<PauseMenu>();

        pauseMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        pauseMenu.TreeExiting += () => ProcessMode = ProcessModeEnum.Always;

        AddChild(pauseMenu);
    }
}