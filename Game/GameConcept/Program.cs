using System.Text;
using GameConcept.Logging;
using GameConcept.Models;
using GameConcept.World;

namespace GameConcept;

internal static class Program
{
    private static void Main()
    {
        Console.InputEncoding = Encoding.UTF8;
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Проклятие заброшенной деревни";

        Console.Write("Введите имя персонажа: ");
        string name = Console.ReadLine() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Странник";
        }

        Hero hero = new(name.Trim());
        GameMap world = new();
        GlobalMap globalMap = new();

        using Logger logger = new("gameLog.txt");
        logger.Show("Проклятие заброшенной деревни. Герой: " + hero.Name);
        logger.Show("Управление: W/A/S/D - ходить, M - глобальная карта, H - зелье, Q - выход.");
        logger.Show("Найдите ключ на старом кладбище и победите некроманта в заброшенном замке.");
        globalMap.Show(world.Locations, world.Index);

        bool playing = true;
        while (playing && hero.Health > 0)
        {
            Location location = world.Current();
            Draw(hero, location);
            Console.Write("Действие: ");
            string action = (Console.ReadLine() ?? "q").Trim().ToLowerInvariant();
            logger.Write("Действие игрока: " + action);

            if (action == "q")
            {
                logger.Show("Выход из игры.");
                break;
            }

            if (action == "h")
            {
                UsePotion(hero, logger);
                continue;
            }

            if (action == "m")
            {
                globalMap.Show(world.Locations, world.Index);
                logger.Write("Игрок открыл глобальную карту.");
                continue;
            }

            int dx = 0;
            int dy = 0;
            if (action == "w")
            {
                dy = -1;
            }
            else if (action == "s")
            {
                dy = 1;
            }
            else if (action == "a")
            {
                dx = -1;
            }
            else if (action == "d")
            {
                dx = 1;
            }
            else
            {
                logger.Show("Неизвестная команда.");
                continue;
            }

            if (!location.Map.Move(dx, dy))
            {
                logger.Show("Туда пройти нельзя.");
                continue;
            }

            logger.Write($"Переход по мини-карте «{location.Name}»: X={location.Map.PlayerX}; Y={location.Map.PlayerY}");
            int cell = location.Map.CurrentCell;

            if (cell == 4)
            {
                TakeItem(hero, location, logger);
            }
            else if (cell == 5)
            {
                VisitMerchant(hero, logger);
            }
            else if (cell == 6)
            {
                Rest(hero, logger);
            }
            else if (cell == 2 && location.Monster is { IsAlive: true } monster)
            {
                playing = Fight(hero, monster, logger);
                if (!playing || hero.Health == 0)
                {
                    continue;
                }

                location.Map.ClearCurrentCell();
                hero.Gold += monster.GoldReward;
                hero.IncreaseDamage(2);
                logger.Show($"Противник побеждён. Получено золота: {monster.GoldReward}. Урон увеличен на 2.");

                if (location.Type == LocationType.Cemetery)
                {
                    hero.HasCastleKey = true;
                    logger.Show("У скелета найден старый ключ от заброшенного замка.");
                }
            }
            else if (cell == 3)
            {
                if (location.Monster is { IsAlive: true })
                {
                    logger.Show("Сначала нужно победить противника в этой локации.");
                }
                else if (world.IsLast())
                {
                    logger.Show("Победа! Некромант повержен, проклятие снято, а жители могут вернуться в деревню.");
                    playing = false;
                }
                else if (world.NextIsCastle() && !hero.HasCastleKey)
                {
                    logger.Show("Ворота замка заперты. Ключ нужно найти у скелета на кладбище.");
                }
                else
                {
                    world.Next();
                    hero.Heal(10);
                    logger.Show("Новая локация: " + world.Current().Name + ". Здоровье: " + hero.Health);
                    logger.Show("Открыта новая мини-карта 3 x 3: " + world.Current().Name + ".");
                    globalMap.Show(world.Locations, world.Index);
                }
            }
        }

        if (hero.Health == 0)
        {
            logger.Show("Игра окончена. Герой погиб, а деревня осталась под проклятием.");
        }
    }

    private static void TakeItem(Hero hero, Location location, Logger logger)
    {
        if (location.HealingBonus > 0)
        {
            hero.Heal(location.HealingBonus);
            logger.Show($"Найден предмет «{location.Item}». Восстановлено здоровья: {location.HealingBonus}. Текущее здоровье: {hero.Health}.");
        }

        if (location.DamageBonus > 0)
        {
            hero.IncreaseDamage(location.DamageBonus);
            logger.Show($"Найден предмет «{location.Item}». Урон героя увеличен до {hero.Damage}.");
        }

        location.Map.ClearCurrentCell();
    }

    private static void VisitMerchant(Hero hero, Logger logger)
    {
        logger.Show("Торговец предлагает лечебное зелье за 5 золотых.");
        Console.Write("1 - купить зелье, 2 - уйти: ");
        string choice = (Console.ReadLine() ?? "2").Trim();
        logger.Write("Выбор у торговца: " + choice);

        if (choice != "1")
        {
            logger.Show("Герой ничего не купил.");
            return;
        }

        if (hero.Gold < 5)
        {
            logger.Show("Недостаточно золота.");
            return;
        }

        hero.Gold -= 5;
        hero.Potions++;
        logger.Show($"Куплено лечебное зелье. Зелий: {hero.Potions}, золота: {hero.Gold}.");
    }

    private static void Rest(Hero hero, Logger logger)
    {
        int restored = 100 - hero.Health;
        hero.Heal(100);
        logger.Show($"Герой отдохнул у костра. Восстановлено здоровья: {restored}. Здоровье: {hero.Health}.");
    }

    private static bool UsePotion(Hero hero, Logger logger)
    {
        if (hero.Potions == 0)
        {
            logger.Show("Лечебных зелий больше нет.");
            return false;
        }

        if (hero.Health == 100)
        {
            logger.Show("Здоровье полное. Зелье осталось в запасе.");
            return false;
        }

        hero.Potions--;
        hero.Heal(30);
        logger.Show($"Герой выпил зелье. Здоровье: {hero.Health}, зелий осталось: {hero.Potions}.");
        return true;
    }

    private static bool Fight(Hero hero, Monster monster, Logger logger)
    {
        logger.Show("На пути противник: " + monster.Name);
        while (hero.Health > 0 && monster.IsAlive)
        {
            ShowCharacterStatus(hero);
            ShowCharacterStatus(monster);
            Console.Write("1 - атака, 2 - зелье, 3 - защита, q - выход: ");
            string action = (Console.ReadLine() ?? "q").Trim().ToLowerInvariant();
            logger.Write("Ход в бою: " + action);
            bool defended = false;

            if (action == "q")
            {
                logger.Show("Игрок вышел во время боя.");
                return false;
            }

            if (action == "1")
            {
                hero.Attack(monster);
                logger.Show($"{hero.Name} атакует. У противника осталось {monster.Health} HP.");
            }
            else if (action == "2")
            {
                if (!UsePotion(hero, logger))
                {
                    continue;
                }
            }
            else if (action == "3")
            {
                defended = true;
                logger.Show("Герой защищается и уменьшает входящий урон вдвое.");
            }
            else
            {
                logger.Show("Неизвестное действие.");
                continue;
            }

            if (monster.IsAlive)
            {
                int before = hero.Health;
                monster.Attack(hero, defended);
                logger.Show(monster.Name + " атакует. Получено урона: " + (before - hero.Health));
            }
        }

        return true;
    }

    private static void Draw(Hero hero, Location location)
    {
        Console.WriteLine();
        Console.WriteLine("=== " + location.Name + " ===");
        Console.WriteLine(location.Description);
        ShowCharacterStatus(hero);
        Console.WriteLine("0 - герой, 1 - путь, 2 - враг, 3 - выход, 4 - предмет, 5 - торговец, 6 - костёр");

        location.Map.Draw();
    }

    private static void ShowCharacterStatus(GameCharacter character)
    {
        Console.WriteLine(character.GetStatus());
    }
}
