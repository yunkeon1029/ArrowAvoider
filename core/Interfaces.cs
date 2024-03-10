public interface IDespawnable
{
    public void Despawn();
}

public interface IDamageable
{
    public void Damage(float damage);
}

public interface IHealable
{
    public void Heal(float healing);
}