using Godot;

internal partial class SettingsMenu : CanvasLayer
{
    [Export]
    private Slider _bgmVolumeSlider;
    [Export]
    private Slider _sfxVolumeSlider;
    [Export]
    private BaseButton _backButton;

    public override void _Ready()
    {
        var saveManager = Singletons.GetInstance<SaveManager>();

        _bgmVolumeSlider.Value = (double?)saveManager.GetData("BgmVolume") ?? 0.5f;
        _sfxVolumeSlider.Value = (double?)saveManager.GetData("SfxVolume") ?? 0.5f;

        _bgmVolumeSlider.ValueChanged += value => saveManager.SetData("BgmVolume", value);
        _sfxVolumeSlider.ValueChanged += value => saveManager.SetData("SfxVolume", value);

        _backButton.Pressed += QueueFree;
    }
}