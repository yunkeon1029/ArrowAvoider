using Godot;
using System;

namespace ArrowAvoider;

[Tool]
public partial class ScoreManager : Node
{
    [Signal]
    public delegate void ScoreChangedEventHandler(int delta);

    [Export]
    public int Score 
    { 
        get => _score;
        set => SetScore(value);
    }

    private int _score;

    public void SetScore(int value)
    {
        int previousScore = _score;

        _score = value;
        _score = Mathf.Max(0, _score);

        if (previousScore != _score)
            EmitSignal(SignalName.ScoreChanged, _score - previousScore);
    }
}