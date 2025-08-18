using Godot;

internal partial class WindowManager : Node, ISingleton
{
    public WindowManager()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);

        Ready += UpdateWindowMode;
    }

    public override void _Process(double delta)
    {
        if (!Input.IsActionJustPressed(ActionName.ToggleFullscreen))
            return;

        bool fullScreen = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen;

        if (fullScreen == true)
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);

        if (fullScreen == false)
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
    }

    public void UpdateWindowMode()
    {
        var saveManager = Singletons.GetInstance<SaveManager>();

        bool fullScreen = (bool?)saveManager.GetData(SaveName.FullScreen) ?? true;
        bool currentFullScreen = DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen;

        if (fullScreen == true && currentFullScreen == false)
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);

        if (fullScreen == false && currentFullScreen == true)
            DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
    }
}
