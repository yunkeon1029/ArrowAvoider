using Godot;
using System;

internal partial class SceneManager : Node, ISingleton
{
	[Signal]
	public delegate void SceneLoadedEventHandler(Node loadedScene);

	public SceneManager()
	{
		TreeEntered += () => Singletons.AddInstance(this);
		TreeExited += () => Singletons.RemoveInstance(this);
	}

	public void ChangeScene(PackedScene scene, Action<Node> sceneLoaded = null)
	{
		SceneTree tree = GetTree();
        Window root = tree.Root;

		Node previousScene = tree.CurrentScene;
        Node sceneInstance = scene.Instantiate();

		sceneLoaded?.Invoke(sceneInstance);
		EmitSignal(SignalName.SceneLoaded, sceneInstance);

		previousScene.TreeExited += () => root.AddChild(sceneInstance);
		previousScene.TreeExited += () => tree.CurrentScene = sceneInstance;
		
		previousScene.QueueFree();
	}

	public void ChangeScene(PackedScene scene, PackedScene transition, Action<Node> sceneLoaded = null)
	{
		TransitionAnimation transitionInstance = transition.Instantiate<TransitionAnimation>();

		Action fadeInEnded = delegate { };
		Action fadeOutEnded = delegate { };

		sceneLoaded += _ => transitionInstance.PlayFadeOutAnimation(fadeOutEnded);

		fadeInEnded += () => ChangeScene(scene, sceneLoaded);
		fadeOutEnded += () => transitionInstance.QueueFree();

		transitionInstance.PlayFadeInAnimation(fadeInEnded);
		AddSibling(transitionInstance);
	}

	public void ChangeScene(string scenePath, Action<Node> sceneLoaded = null)
	{
		PackedScene loadingScene = GD.Load<PackedScene>(scenePath);
		ChangeScene(loadingScene, sceneLoaded);
	}

	public void ChangeScene(string scenePath, PackedScene transition, Action<Node> sceneLoaded = null)
	{
		PackedScene loadingScene = GD.Load<PackedScene>(scenePath);
		ChangeScene(loadingScene, transition, sceneLoaded);
	}
}