using Godot;
using System;

internal partial class AudioManager : Node, ISingleton
{
    private AudioStreamPlayer _musicPlayer;

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

    public void PlayMusic(AudioStream audioStream, Action<AudioStreamPlayer> modifyMusic = null)
    {
        _musicPlayer?.QueueFree();
		_musicPlayer = new();

        AddChild(_musicPlayer);

        _musicPlayer.Stream = audioStream;
        _musicPlayer.Bus = "Music";
        _musicPlayer.Play();

        modifyMusic?.Invoke(_musicPlayer);
    }

    public void PlaySFX(AudioStream audioStream, Action<AudioStreamPlayer> modifySFX = null)
    {
        AudioStreamPlayer instance = new();
        AddChild(instance);

        instance.Stream = audioStream;
        instance.Bus = "SFX";
        instance.Play();

        modifySFX?.Invoke(instance);
        instance.Finished += () => instance.QueueFree();
    }

    public AudioStream GetPlayingMusic()
    {
        if (_musicPlayer == null)
            return null;

        if (_musicPlayer.Playing == false)
            return null;

        return _musicPlayer.Stream;
    }
}