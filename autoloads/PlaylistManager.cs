using System.Linq;
using Godot;

internal partial class PlaylistManager : Node, ISingleton
{
    [Export]
    public AudioStream[] Playlist;

	public PlaylistManager()
	{
		TreeEntered += () => Singletons.AddInstance(this);
        TreeExited += () => Singletons.RemoveInstance(this);
	}
	
    public void PlayNextSong(AudioStream previousMusic = null)
    {
        var nextPlayableMusic = Playlist?.Where(music => music != previousMusic || Playlist.Length <= 1)
                                         .ToArray();

        if (nextPlayableMusic == null)
            return;                            

        if (nextPlayableMusic.Length != 0)
        {
            int nextMusicIndex = GD.RandRange(0, nextPlayableMusic.Length - 1);

            var audioManager = Singletons.GetInstance<AudioManager>();
            var nextMusic = nextPlayableMusic[nextMusicIndex];

            audioManager.PlayMusic(nextMusic, ModifyMusic);
        }
    }

    private void ModifyMusic(AudioStreamPlayer musicPlayer)
    {
        var audioManager = Singletons.GetInstance<AudioManager>();
        var currentMusic = audioManager.GetPlayingMusic();

        musicPlayer.Finished += () => PlayNextSong(currentMusic);
    }
}