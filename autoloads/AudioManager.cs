using Godot;
using System;

internal partial class AudioManager : Node, ISingleton
{
    public AudioManager()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);
    }

    public void PlayAudio(AudioStream audioStream, Action<AudioStreamPlayer> modifySound = null)
    {
        AudioStreamPlayer instance = new();
        AddChild(instance);

        instance.Stream = audioStream;
        instance.Play();

        modifySound?.Invoke(instance);
        instance.Finished += () => instance.QueueFree();
    }
}