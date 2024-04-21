using Godot;

internal partial class SettingsWindow : CanvasLayer
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

        _bgmVolumeSlider.Value = (double?)saveManager.GetData("BGMVolume") ?? 0.5f;
        _sfxVolumeSlider.Value = (double?)saveManager.GetData("SFXVolume") ?? 0.5f;

        _bgmVolumeSlider.ValueChanged += value => saveManager.SetData("BGMVolume", value);
        _sfxVolumeSlider.ValueChanged += value => saveManager.SetData("SFXVolume", value);

        _backButton.Pressed += QueueFree;
    }
}
