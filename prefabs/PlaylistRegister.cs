using Godot;
using System;
using System.Linq;

internal partial class PlaylistRegister : Node
{
    [Export]
    private AudioStream[] _playlist;

    public override void _Ready()
    {
        var playlistManager = Singletons.GetInstance<PlaylistManager>();
        var audioManager = Singletons.GetInstance<AudioManager>();
        var currentMusic = audioManager.GetPlayingMusic();

        playlistManager.Playlist = _playlist;

        if (_playlist.Contains(currentMusic) == false)
            playlistManager.PlayNextSong();
    }
}