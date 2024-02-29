using Godot;

namespace ArrowAvoider;

public partial class GameManager : Node
{
    [Export]
    private PlayerController _playerController;
    [Export]
    private ScoreManager _scoreManager;
    [Export]
    private GameUI _gameUI;

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
        ResultMenuManager resultMenuManager = resultMenuScene.Instantiate<ResultMenuManager>();

        root.AddChild(resultMenuManager);
        tree.CurrentScene = resultMenuManager;
        resultMenuManager.NotifyScore(_scoreManager.Score);

        QueueFree();
    }
}

