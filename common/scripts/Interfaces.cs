internal interface IDamageable
{
    public void Damage(float damage);
}

internal interface IHealable
{
    public void Heal(float healing);
}

internal interface IDespawnable { }
internal interface ISingleton { }