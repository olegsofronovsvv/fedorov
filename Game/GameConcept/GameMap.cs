namespace GameConcept.World;

using GameConcept.Models;

internal sealed class GameMap
{
    public List<Location> Locations;
    public int Index;

    public GameMap()
    {
        Locations =
        [
            new Location(
                "Заброшенная деревня",
                "Жители исчезли. Здесь остались торговец и безопасный костёр для отдыха.",
                null,
                "Нет предмета",
                damageBonus: 0,
                healingBonus: 0,
                new LocalMap(new int[,]
                {
                    { 1, 1, 1 },
                    { 5, 1, 6 },
                    { 1, 1, 3 }
                }),
                LocationType.Village),

            new Location(
                "Тёмный лес",
                "В густом лесу рыщет дикий волк. Среди деревьев растут лечебные травы.",
                new Wolf(),
                "Лечебные травы",
                damageBonus: 0,
                healingBonus: 25,
                new LocalMap(new int[,]
                {
                    { 1, 2, 1 },
                    { 1, 1, 1 },
                    { 4, 1, 3 }
                }),
                LocationType.Forest),

            new Location(
                "Старое кладбище",
                "Оживший скелет-воин охраняет ключ от заброшенного замка.",
                new SkeletonWarrior(),
                "Ржавый меч",
                damageBonus: 5,
                healingBonus: 0,
                new LocalMap(new int[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 4, 1, 3 }
                }),
                LocationType.Cemetery),

            new Location(
                "Заброшенный замок",
                "В тронном зале находится древний некромант, наложивший проклятие на деревню.",
                new Necromancer(),
                "Священный амулет",
                damageBonus: 7,
                healingBonus: 0,
                new LocalMap(new int[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 },
                    { 4, 1, 3 }
                }),
                LocationType.Castle)
        ];
    }

    public Location Current()
    {
        return Locations[Index];
    }

    public bool IsLast()
    {
        return Index == Locations.Count - 1;
    }

    public bool NextIsCastle()
    {
        return Index + 1 < Locations.Count && Locations[Index + 1].Type == LocationType.Castle;
    }

    public void Next()
    {
        Index++;
    }
}
