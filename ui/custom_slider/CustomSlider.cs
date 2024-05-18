using System.Runtime.InteropServices;
using Godot;

internal partial class CustomSlider : HSlider
{
	[Export]
	private Panel _grabber;
	[Export]
	private Label _valueLabel;

    public override void _Ready()
    {
		ValueChanged += _ => UpdateSlider();
		UpdateSlider();
    }

    private void UpdateSlider()
	{
		float valueRatio = (float)(1 / MaxValue * Value);
		float sliderOffset = Size.X * valueRatio;

		Vector2 centerPos = new(sliderOffset, Size.Y / 2);

		_valueLabel.SetCenter(centerPos);
		_valueLabel.Text = Mathf.Round(Value).ToString();

		_grabber.SetCenter(centerPos);
	}
}
