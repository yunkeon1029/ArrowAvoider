using Godot;

namespace ArrowAvoider;

public partial class GameScreen : Node
{
    [Export]
    private Player _playerController;
    [Export]
    private ScoreManager _scoreManager;
    [Export]
    private GameScreenUI _gameUI;

    [Export(PropertyHint.File, "*.tscn")]
    private string _resultMenuPath;

    public override void _Ready()
    {
        HealthComponent playerHealthComponent = _playerController.HealthComponent;

        _gameUI.InjectDependencies(playerHealthComponent, _scoreManager);
        playerHealthComponent.HealthDepleted += LoadResultMenu;
    }

    public void LoadResultMenu()
    {
        SceneTree tree = GetTree();
        Window root = tree.Root;

        PackedScene resultMenuScene = GD.Load<PackedScene>(_resultMenuPath);
        ResultScreen resultMenuManager = resultMenuScene.Instantiate<ResultScreen>();

        root.AddChild(resultMenuManager);
        tree.CurrentScene = resultMenuManager;
        resultMenuManager.NotifyScore(_scoreManager.Score);

        QueueFree();
    }
}

