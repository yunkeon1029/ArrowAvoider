using Godot;

internal partial class DespawnArea : Area2D
{
    [Signal]
    public delegate void DespawnedNodeEventHandler(Node2D despawnedNode);

    public override void _Ready()
    {
        AreaEntered += OnDectection;
        BodyEntered += OnDectection;
    }

    private void OnDectection(Node2D dectectedNode)
    {
        if (!dectectedNode.IsInGroup("Despawnable"))
            return;

        dectectedNode.QueueFree();
        EmitSignal(SignalName.DespawnedNode, dectectedNode);
    }
}