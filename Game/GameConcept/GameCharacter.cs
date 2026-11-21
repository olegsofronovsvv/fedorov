using GameConcept.Contracts;

namespace GameConcept.Models;

internal abstract class GameCharacter : IDamageable, IAttacker
{
    protected GameCharacter(string name, int health, int damage)
    {
        Name = name;
        Health = health;
        Damage = damage;
    }

    public string Name { get; }
    public int Health { get; protected set; }
    public int Damage { get; protected set; }
    public bool IsAlive => Health > 0;

    public virtual void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - Math.Max(0, amount));
    }

    public virtual void Attack(IDamageable target)
    {
        target.TakeDamage(Damage);
    }

    public abstract string GetStatus();
}
