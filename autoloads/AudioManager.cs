using Godot;
using System;

internal partial class AudioManager : Node, ISingleton
{
    public AudioManager()
    {
        TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);

        Ready += UpdateVolume;
    }

    public void UpdateVolume()
    {
        var saveManager = Singletons.GetInstance<SaveManager>();

        int bgmBusIndex = AudioServer.GetBusIndex("Music");
        int sfxBusIndex = AudioServer.GetBusIndex("SFX");

        float musicVolume = (float?)saveManager.GetData("MusicVolume") ?? 0.5f;
        float sfxVolume = (float?)saveManager.GetData("SfxVolume") ?? 0.5f;

        AudioServer.SetBusVolumeDb(bgmBusIndex, Mathf.LinearToDb(musicVolume));
        AudioServer.SetBusVolumeDb(sfxBusIndex, Mathf.LinearToDb(sfxVolume));
    }

    public void PlaySFX(AudioStream audioStream, Action<AudioStreamPlayer> modifySound = null)
    {
        AudioStreamPlayer instance = new();
        AddChild(instance);

        instance.Stream = audioStream;
        instance.Bus = "SFX";
        instance.Play();

        modifySound?.Invoke(instance);
        instance.Finished += () => instance.QueueFree();
    }
}