using Godot;

internal partial class MainMenuUI : CanvasLayer
{
	[Export]
	public BaseButton StartButton { get; private set; }
	[Export]
	public BaseButton SettingsButton { get; private set; }
	[Export]
	public BaseButton ExitButton { get; private set; }

	// add SeeScoreButton?
}
