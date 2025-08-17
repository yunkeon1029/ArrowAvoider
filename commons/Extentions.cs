using Godot;

internal static class Extentions
{
    public static Vector2 GetCenter(this Control control)
    {
        return control.Position + control.Size / 2;
    }

    public static void SetCenter(this Control control, Vector2 pos)
    {
        control.Position = pos - control.Size / 2;
    }
}