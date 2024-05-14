using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

internal partial class HeartContainer : HBoxContainer
{
	[Export]
	private PackedScene _heart;

	public void Update(int maxHealth, int health)
	{
		foreach (Node child in GetChildren())
			child.QueueFree();

		for (int i = 0; i < maxHealth; i++)
		{
			Heart heart = _heart.Instantiate<Heart>();

			heart.UpdateHeart(i < health);
			AddChild(heart);
		}
	}

	public void Update(int health)
	{
		var hearts = GetChildren().OfType<Heart>();

		for (int i = 0; i < hearts.Count(); i++)
		{
			Heart heart = hearts.ElementAt(i);
			heart.UpdateHeart(i < health);
		}
	}
}
