using Godot;
using System.Linq;

internal partial class HeartContainer : HBoxContainer
{
    [Export]
    private PackedScene _heartBar;

    public void Update(float maxHealth, float health)
    {
        foreach (Node child in GetChildren())
            child.QueueFree();

        for (int i = 0; i < maxHealth; i++)
        {
            Range heartBar = _heartBar.Instantiate<Range>();

            heartBar.Value = Mathf.Clamp(health - i, 0, 1);
            AddChild(heartBar);
        }
    }
}
