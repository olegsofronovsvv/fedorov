using GameConcept.Models;

namespace GameConcept.World;
internal sealed class Location
{
    public string Name;
    public string Description;
    public Monster? Monster;
    public string Item;
    public int DamageBonus;
    public int HealingBonus;
    public LocalMap Map;
    public LocationType Type;

    public Location(
        string name,
        string description,
        Monster? monster,
        string item,
        int damageBonus,
        int healingBonus,
        LocalMap map,
        LocationType type)
    {
        Name = name;
        Description = description;
        Monster = monster;
        Item = item;
        DamageBonus = damageBonus;
        HealingBonus = healingBonus;
        Map = map;
        Type = type;
    }
}

internal enum LocationType
{
    Village,
    Forest,
    Cemetery,
    Castle
}
