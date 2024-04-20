using Godot;
using System;

internal partial class SettingsWindow : CanvasLayer
{
	[Export]
    public BaseButton BackButton { get; private set; }
    [Export]
    public Slider BGMSizeSlider { get; private set; }
    [Export]
    public Slider SFXSizeSlider { get; private set; }

    public override void _Ready()
    {
        var saveManager = Singletons.GetInstance<SaveManager>();

        BGMSizeSlider.Value = (double?)saveManager.GetData("BGMSize") ?? 50f;
        SFXSizeSlider.Value = (double?)saveManager.GetData("SFXSize") ?? 50f;

        BGMSizeSlider.ValueChanged += value => saveManager.SetData("BGMSize", value);
        SFXSizeSlider.ValueChanged += value => saveManager.SetData("SFXSize", value);

        BackButton.Pressed += QueueFree;
    }
}
