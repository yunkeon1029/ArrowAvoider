using Godot;

internal partial class SettingsMenu : CanvasLayer
{
    [Export]
    private Slider _musicVolumeSlider;
    [Export]
    private Slider _sfxVolumeSlider;
    [Export]
    private CheckBox _fullScreenCheckBox;
    [Export]
    private BaseButton _backButton;

    private SaveManager _saveManager;
    private AudioManager _audioManager;
    private WindowManager _windowManager;

    public override void _Ready()
    {
        _saveManager = Singletons.GetInstance<SaveManager>();
        _audioManager = Singletons.GetInstance<AudioManager>();
        _windowManager = Singletons.GetInstance<WindowManager>();

        _musicVolumeSlider.Value = (double?)_saveManager.GetData("MusicVolume") ?? 0.5f;
        _sfxVolumeSlider.Value = (double?)_saveManager.GetData("SfxVolume") ?? 0.5f;
        _fullScreenCheckBox.ButtonPressed = (bool?)_saveManager.GetData("FullScreen") ?? true;

        _musicVolumeSlider.ValueChanged += value => _saveManager.SetData("MusicVolume", value);
        _sfxVolumeSlider.ValueChanged += value => _saveManager.SetData("SfxVolume", value);

        _musicVolumeSlider.ValueChanged += _ => _audioManager.UpdateVolume();
        _sfxVolumeSlider.ValueChanged += _ => _audioManager.UpdateVolume();

        _fullScreenCheckBox.Toggled += FullScreenCheckBoxToggled;
        _backButton.Pressed += QueueFree;
    }

    public override void _Process(double elapsedTime)
	{
		if (Input.IsActionJustPressed(ActionName.Escape))
            QueueFree();
	}

    private void FullScreenCheckBoxToggled(bool value)
    {
        _saveManager.SetData("FullScreen", value);
        _windowManager.UpdateWindowMode();
    }
}
