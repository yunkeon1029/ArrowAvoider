using Godot;

internal partial class SettingsMenu : CanvasLayer
{
    [Export]
    private Slider _musicVolumeSlider;
    [Export]
    private Slider _sfxVolumeSlider;
    [Export]
    private BaseButton _backButton;

    public override void _Ready()
    {
        var saveManager = Singletons.GetInstance<SaveManager>();
        var audioManager = Singletons.GetInstance<AudioManager>();

        _musicVolumeSlider.Value = (double?)saveManager.GetData("MusicVolume") ?? 0.5f;
        _sfxVolumeSlider.Value = (double?)saveManager.GetData("SfxVolume") ?? 0.5f;

        _musicVolumeSlider.ValueChanged += value => saveManager.SetData("MusicVolume", value);
        _sfxVolumeSlider.ValueChanged += value => saveManager.SetData("SfxVolume", value);
        
        _musicVolumeSlider.ValueChanged += _ => audioManager.UpdateVolume();
        _sfxVolumeSlider.ValueChanged += _ => audioManager.UpdateVolume();

        _backButton.Pressed += QueueFree;
    }
}
