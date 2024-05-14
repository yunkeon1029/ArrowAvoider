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

        healthManager.HealthChanged += _ => UpdateHeartContainer();
        healthManager.MaxHealthChanged += _ => UpdateHeartContainer();
        healthManager.HealthDepleted += () => sceneManager.ChangeScene(_resultMenuScenePath, OnSceneLoaded);

        ScoreManager.ScoreChanged += _ => GameUI.UpdateScoreLabel(ScoreManager.Score);

		GameUI.UpdateHeartContainer((int)healthManager.MaxHealth, (int)healthManager.Health);
		GameUI.UpdateScoreLabel(ScoreManager.Score);

		RequestReady();
    }

    public override void _Process(double elapsedTime)
    {
		if (!Input.IsActionJustPressed(ActionName.Pause))
			return;

		OpenPauseMenu();
    }

    private void UpdateHeartContainer()
	{
        var healthManager = Player.HealthManager;

		int maxHealth = (int)healthManager.MaxHealth;
		int health = (int)healthManager.Health;

		GameUI.UpdateHeartContainer(maxHealth, health);
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

        AddChild(pauseMenu);
    }
}