using Godot;

internal partial class GameUI : CanvasLayer
{
    [Export]
    private HeartContainer _heartContainer;
    [Export]
    private Label _scoreLabel;

    public void UpdateHeartContainer(int maxHealth, int health)
    {
        _heartContainer.Update(maxHealth, health);
    }

    public void UpdateScoreLabel(float score)
    {
        _scoreLabel.Text = $"score: {score}";
    }
}