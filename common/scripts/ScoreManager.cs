using Godot;

public partial class ScoreManager : Node
{
    [Signal]
    public delegate void ScoreChangedEventHandler(float delta);

    public int Score
    {
        get => _score;
        set => SetScore(value);
    }

    private int _score;

    public ScoreManager()
    {
        TreeEntered += () => GlobalInstances.AddInstance(this);
        TreeExited += () => GlobalInstances.RemoveInstance(this);
    }

    public void SetScore(int score)
    {
        float previousScore = _score;

        _score = score;
        _score = Mathf.Max(_score, 0);

        if (_score != previousScore)
            EmitSignal(SignalName.ScoreChanged, _score - previousScore);
    }
}