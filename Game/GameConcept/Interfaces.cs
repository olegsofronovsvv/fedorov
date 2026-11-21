namespace GameConcept.Contracts;

internal interface IDamageable
{
    int Health { get; }
    bool IsAlive { get; }

    void TakeDamage(int amount);
}

internal interface IAttacker
{
    int Damage { get; }

    void Attack(IDamageable target);
}

internal interface IMovable
{
    bool Move(int dx, int dy);
}
