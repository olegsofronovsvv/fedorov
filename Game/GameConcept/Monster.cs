namespace GameConcept.Models;

internal abstract class Monster : GameCharacter
{
    public int GoldReward { get; }

    protected Monster(string name, int health, int damage, int goldReward)
        : base(name, health, damage)
    {
        GoldReward = goldReward;
    }

    public void Attack(Hero hero, bool defended)
    {
        int amount = defended ? Damage / 2 : Damage;
        hero.TakeDamage(amount);
    }

    public override string GetStatus()
    {
        return $"Монстр {Name}: {Health} HP, {Damage} урона, награда: {GoldReward}";
    }
}
