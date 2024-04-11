using Godot;

internal partial class GameUI : CanvasLayer
{
    [Export]
    private Label _healthLabel;
    [Export]
    private Label _scoreLabel;

    public void UpdateHealthLabel(float maxHealth, float health)
    {
        _healthLabel.Text = $"max health: {maxHealth} \n" + 
                            $"health: {health}";
    }

    public void UpdateScoreLabel(float score)
    {
        _scoreLabel.Text = $"score: {score}";
    }
}