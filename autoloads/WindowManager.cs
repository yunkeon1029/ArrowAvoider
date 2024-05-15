using Godot;

internal partial class WindowManager : Node, ISingleton
{
    public WindowManager()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);

        Ready += UpdateWindowMode;
    }

	public void UpdateWindowMode()
	{
		var saveManager = Singletons.GetInstance<SaveManager>();

		bool fullScreen = (bool?)saveManager.GetData("FullScreen") ?? true;

		if (fullScreen == true)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);

		if (fullScreen == false)
			DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
	}
}
