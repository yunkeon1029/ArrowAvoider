using Godot;

namespace ArrowAvoider;

public partial class PlayerHealthRegenerator : Node
{
	[Export]
	private HealthComponent _healthComponent;
	[Export]
	private float _healthRegenRate;
	[Export]
	private float _healthRegenDelay;

	public void CalculateHealthRegen(float? lastDamagedTime, double elapsedTIme)
	{
		float healthRegenAmount = _healthRegenRate * (float)elapsedTIme;

		if (lastDamagedTime == null || lastDamagedTime >= _healthRegenDelay)
			_healthComponent.ChangeHealth(healthRegenAmount);
	}
}