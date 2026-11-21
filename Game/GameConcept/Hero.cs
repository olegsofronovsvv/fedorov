namespace GameConcept.Models;

internal sealed class Hero : GameCharacter
{
    public int Potions { get; set; }
    public int Gold { get; set; }
    public bool HasCastleKey { get; set; }

    public Hero(string name) : base(name, 100, 20)
    {
        Potions = 2;
        Gold = 5;
    }

    public void Heal(int amount)
    {
        Health = Math.Min(100, Health + Math.Max(0, amount));
    }

    public void IncreaseDamage(int amount)
    {
        Damage += Math.Max(0, amount);
    }

    public override string GetStatus()
    {
        return $"Герой {Name}: {Health} HP, {Damage} урона, зелий: {Potions}, золота: {Gold}";
    }
}
