using Godot;

public partial class GameUI : CanvasLayer
{
    [Export]
    private Label _healthLabel;
    [Export]
    private Label _scoreLabel;
    [Export]
    private Label _deadLabel;

    private HealthManager _playerHealthManager;
    private ScoreManager _scoreManager;

    public override void _Ready()
    {
        Player player = GlobalInstances.GetInstance<Player>();

        _playerHealthManager = player.HealthManager;
        _playerHealthManager.HealthChanged += _ => UpdateHealthLabel();

        _scoreManager = GlobalInstances.GetInstance<ScoreManager>();
        _scoreManager.ScoreChanged += _ => UpdateScoreLabel();

        _playerHealthManager.HealthDepleted += () => _deadLabel.Visible = true;

        UpdateHealthLabel();
        UpdateScoreLabel();
    
        RequestReady();
    }

    public void UpdateHealthLabel()
    {
        float health = _playerHealthManager.Health;
        float maxHealth = _playerHealthManager.MaxHealth;

        _healthLabel.Text =  $"max health: {maxHealth} \n" + 
                             $"health: {health}";
    }

    public void UpdateScoreLabel()
    {
        _scoreLabel.Text = $"score: {_scoreManager.Score}";
    }
}