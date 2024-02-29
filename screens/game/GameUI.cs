using Godot;

namespace ArrowAvoider;

public partial class GameUI : CanvasLayer
{
    [Export]
    private Label _scoreLabel;
    [Export]
    private TextureProgressBar _healthBar;

    private HealthComponent _playerHealthComponent;
    private ScoreManager _scoreManager;

    public void InjectDependencies(HealthComponent playerHealthComponent, ScoreManager scoreManager)
    {
        _playerHealthComponent = playerHealthComponent;
        _scoreManager = scoreManager;

        _playerHealthComponent.HealthChanged += _ => UpdateHealthBar();
        _playerHealthComponent.MaxHealthChanged += _ => UpdateHealthBar();

        _scoreManager.ScoreChanged += _ => UpdateScoreLabel();

        UpdateHealthBar();
        UpdateScoreLabel();
    }

    public void UpdateHealthBar()
    {
        _healthBar.MaxValue = _playerHealthComponent.MaxHealth;
        _healthBar.Value = _playerHealthComponent.Health;
    }

    public void UpdateScoreLabel()
    {
        _scoreLabel.Text = $"{_scoreManager.Score} points";
    }
}