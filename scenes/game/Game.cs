using Godot;
using System;

internal partial class Game : Node, ISingleton
{
    [Export]
    public Player Player { get; private set; }
    [Export]
    public ScoreManager ScoreManager { get; private set; }
    [Export]
    public GameUI GameUI { get; private set; }

    [Export(PropertyHint.File, "*.tscn")]
    private string _resultMenuScenePath;
    [Export(PropertyHint.File, "*.tscn")]
    private string _gameScenePath;
    [Export]
    private PackedScene _pauseMenu;

    public Game()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);
    }

    public override void _Ready()
    {
        var sceneManager = Singletons.GetInstance<SceneManager>();
        var healthManager = Player.HealthManager;

        healthManager.HealthChanged += _ => UpdateHealthLabel();
        healthManager.MaxHealthChanged += _ => UpdateHealthLabel();
        healthManager.HealthDepleted += () => sceneManager.ChangeScene(_resultMenuScenePath, OnSceneLoaded);

        ScoreManager.ScoreChanged += _ => GameUI.UpdateScoreLabel(ScoreManager.Score);

		GameUI.UpdateHealthLabel(healthManager.MaxHealth, healthManager.Health);
		GameUI.UpdateScoreLabel(ScoreManager.Score);

		RequestReady();
    }

    public override void _Process(double elapsedTime)
    {
		if (!Input.IsActionJustPressed(ActionName.Pause))
			return;

		OpenPauseMenu();
    }

    private void UpdateHealthLabel()
	{
        var healthManager = Player.HealthManager;

		float maxHealth = healthManager.MaxHealth;
		float health = healthManager.Health;

		GameUI.UpdateHealthLabel(maxHealth, health);
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
        var sceneManager = Singletons.GetInstance<SceneManager>();
		var pauseMenu = _pauseMenu.Instantiate<PauseMenu>();

        pauseMenu.TreeEntered += () => ProcessMode = ProcessModeEnum.Disabled;
        pauseMenu.TreeExited += () => ProcessMode = ProcessModeEnum.Inherit;
        pauseMenu.RequestedRestart += () => ProcessMode = ProcessModeEnum.Always;
        pauseMenu.RequestedRestart += () => sceneManager.ChangeScene(_gameScenePath);

        AddChild(pauseMenu);
    }
}