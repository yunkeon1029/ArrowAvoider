using Godot;

internal partial class Heart : TextureRect
{
	[Export]
	private Texture2D _fullHeart;
	[Export]
	private Texture2D _emptyHeart;

	public void UpdateHeart(bool isFull)
	{
		if (isFull == true)
			Texture = _fullHeart;

		if (isFull == false)
			Texture = _emptyHeart;
	}
}
