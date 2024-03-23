using Godot;
using System;

internal partial class Game : Node, ISingleton
{
    [Export]
    public Player Player { get; private set; }
    [Export]
    public GameUI GameUI { get; private set; }
    [Export]
    public ScoreManager ScoreManager { get; private set; }

    [Export(PropertyHint.File, "*.tscn")]
    private string _resultScenePath;

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
        healthManager.HealthDepleted += () => sceneManager.ChangeScene(_resultScenePath, OnSceneLoaded);

        ScoreManager.ScoreChanged += _ => GameUI.UpdateScoreLabel(ScoreManager.Score);

		GameUI.UpdateHealthLabel(healthManager.MaxHealth, healthManager.Health);
		GameUI.UpdateScoreLabel(ScoreManager.Score);

		RequestReady();
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

        resultMenu.NotifyScore(ScoreManager.Score);
    }
}