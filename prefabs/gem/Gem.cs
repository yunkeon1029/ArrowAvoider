using Godot;

internal partial class Gem : AnimatableBody2D, IDespawnable
{
	[Export]
	private Area2D _hitArea;
	[Export]
	private AudioStream _collectedSound;

	[Export]
	private float _minCollectedSoundPitch;
	[Export]
	private float _maxCollectedSoundPitch;

	[Export]
	private int _score;

	private ScoreManager _scoreManager;
	private AudioManager _audioManager;

    public override void _Ready()
    {
		Game game = Singletons.GetInstance<Game>();

		_hitArea.AreaEntered += OnHit;
		_hitArea.BodyEntered += OnHit;

		_audioManager = Singletons.GetInstance<AudioManager>();
		_scoreManager = game.ScoreManager;

		RequestReady();
    }

    private void OnHit(Node2D hitObject)
	{
		if (hitObject is not Player)
			return;

		_scoreManager.Score += _score;
		_audioManager.PlaySFX(_collectedSound, RandomizePitch);

		QueueFree();
	}

	private void RandomizePitch(AudioStreamPlayer audioPlayer)
	{
		float minPitch = _minCollectedSoundPitch;
		float maxPitch = _maxCollectedSoundPitch;

		audioPlayer.PitchScale = (float)GD.RandRange(minPitch, maxPitch);
	}
}