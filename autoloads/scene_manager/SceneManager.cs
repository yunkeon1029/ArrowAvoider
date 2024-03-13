using Godot;
using System;

public partial class SceneManager : Node
{
	[Signal]
	public delegate void SceneLoadedEventHandler(Node loadedScene);

	public SceneManager()
	{
		TreeEntered += () => Singletons.AddInstance(this);
		TreeExited += () => Singletons.RemoveInstance(this);
	}

	public void ChangeScene(PackedScene loadingScene, Action<Node> sceneLoaded = null)
	{
		SceneTree tree = GetTree();
        Window root = tree.Root;

		Node previousScene = tree.CurrentScene;
        Node sceneInstance = loadingScene.Instantiate();

		sceneLoaded?.Invoke(previousScene);
		EmitSignal(SignalName.SceneLoaded, sceneInstance);

		previousScene.TreeExited += () => root.AddChild(sceneInstance);
		previousScene.TreeExited += () => tree.CurrentScene = sceneInstance;
		
		previousScene.QueueFree();
	}

	public void ChangeScene(string loadingScenePath, Action<Node> sceneLoaded = null)
	{
		PackedScene loadingScene = GD.Load<PackedScene>(loadingScenePath);
		ChangeScene(loadingScene, sceneLoaded);
	}
}
