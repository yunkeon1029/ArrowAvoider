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
	
    public void PlayNextSong()
    {
        var audioManager = Singletons.GetInstance<AudioManager>();
        var currentMusic = audioManager.GetPlayingMusic();

        var nextPlayableMusic = Playlist?.Where(music => music != currentMusic || Playlist.Length <= 1)
                                         .ToArray();

        if (nextPlayableMusic == null)
            return;                            

        if (nextPlayableMusic.Length != 0)
        {
            int nextMusicIndex = GD.RandRange(0, nextPlayableMusic.Length - 1);
            var nextMusic = nextPlayableMusic[nextMusicIndex];

            audioManager.PlayMusic(nextMusic, musicPlayer => musicPlayer.Finished += PlayNextSong);
        }
    }
}