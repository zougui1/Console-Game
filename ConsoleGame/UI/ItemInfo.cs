using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.utils;
using ConsoleGame.UI.menus;

namespace ConsoleGame.UI
{
    public delegate void DisplayItem(Item item);
    public delegate void DisplayArmor(Armor armor);
    public delegate void DisplayShield(Shield shield);
    public delegate void DisplayWeapon(Weapon weapon);

    public class ItemInfo
    {
        public Item Item { get; set; }
        public DisplayItem DisplayItem { get; set; } = DefaultDisplayItem;
        public DisplayArmor DisplayArmor { get; set; } = DefaultDisplayArmor;
        public DisplayShield DisplayShield { get; set; } = DefaultDisplayShield;
        public DisplayWeapon DisplayWeapon { get; set; } = DefaultDisplayWeapon;

        public void Display()
        {
            Console.CursorVisible = false;
            if (Item.GetType() == typeof(Item))
            {
                DisplayItem(Item);
            }
            else if (Item.GetType() == typeof(Armor))
            {
                DisplayArmor((Armor)Item);
            }
            else if (Item.GetType() == typeof(Shield))
            {
                DisplayShield((Shield)Item);
            }
            else if (Item.GetType() == typeof(Weapon))
            {
                DisplayWeapon((Weapon)Item);
            }

            Utils.Cconsole.Absolute().Left(50).Top(Console.WindowHeight - 2).Write("Press Enter to leave");
            Utils.ConsoleClearer();
            Console.CursorVisible = true;
        }

        public static void DefaultDisplayItem(Item item)
        {
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Throw", (object t) => { })
                .AddChoice("Move", (object t) => { });
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();

            Utils.Cconsole
                .Left(50).WriteLine(item.Name)
                .Absolute().Write("Description:").Absolute().Offset(20).WriteLine(item.Description)
                .Absolute().Write("Value:").Absolute().Offset(20).WriteLine("{0} GP", item.Coins);

            if (item.IsHeal)
            {
                Utils.Cconsole
                    .Absolute().Write("Can restore {0} HP", item.Restore);
            }
            else if (item.IsMana)
            {
                Utils.Cconsole
                    .Absolute().Write("Can restore {0} mana", item.Restore);
            }
        }

        public static void DefaultDisplayArmor(Armor armor)
        {
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Throw", (object t) => { })
                .AddChoice("Move", (object t) => { })
                .AddChoice("Equip", (object t) => { });
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();

            Utils.Cconsole
                .Left(50).WriteLine(armor.Name)
                .Absolute().Write("Description:").Absolute().Offset(20).WriteLine(armor.Description)
                .Absolute().Write("Value:").Absolute().Offset(20).WriteLine("{0} GP", armor.Coins)
                .Absolute().Write("Category:").Absolute().Offset(20).WriteLine(armor.SubCategory)
                .Absolute().Write("Defense:").Absolute().Offset(20).WriteLine(armor.Defense);

            DisplayBonusesAndMaluses(armor.Stats);
        }

        public static void DefaultDisplayShield(Shield shield)
        {
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Throw", (object t) => { })
                .AddChoice("Move", (object t) => { })
                .AddChoice("Equip", (object t) => { });
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();

            Utils.Cconsole
                .Left(50).WriteLine(shield.Name)
                .Absolute().Write("Description:").Absolute().Offset(20).WriteLine(shield.Description)
                .Absolute().Write("Value:").Absolute().Offset(20).WriteLine("{0} GP", shield.Coins)
                .Absolute().Write("Category:").Absolute().Offset(20).WriteLine(shield.SubCategory)
                .Absolute().Write("Damages:").Absolute().Offset(20).WriteLine(shield.Damages)
                .Absolute().Write("Defense:").Absolute().Offset(20).WriteLine(shield.Defense);

            if (shield.TwoHanded)
            {
                Utils.Cconsole.Absolute().WriteLine("This shield is two handed");
            }

            DisplayBonusesAndMaluses(shield.Stats);
        }

        public static void DefaultDisplayWeapon(Weapon weapon)
        {
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Throw", (object t) => { })
                .AddChoice("Move", (object t) => { })
                .AddChoice("Equip", (object t) => { });
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.ColorOdd = "#ccc";

            menu.LineChanged += (int line) =>
            {
                Utils.SetTimeout(() =>
                {
                    Console.CursorTop = 0;
                    Utils.Cconsole
                        .Offset(60).Absolute().WriteLine(weapon.Name).NewLine()
                        .Offset(35).Absolute().Write("Description:").Absolute().Offset(55).WriteLine(weapon.Description)
                        .Offset(35).Absolute().Write("Value:").Absolute().Offset(55).WriteLine("{0} GP", weapon.Coins)
                        .Offset(35).Absolute().Write("Category:").Absolute().Offset(55).WriteLine(weapon.SubCategory)
                        .Offset(35).Absolute().Write("Damages:").Absolute().Offset(55).WriteLine(weapon.Damages);

                    if (weapon.TwoHanded)
                    {
                        Utils.Cconsole.Absolute().Offset(35).WriteLine("This weapon is two handed");
                    }

                    DisplayBonusesAndMaluses(weapon.Stats);
                }, 100);
            };
            menu.InitSelection();
        }

        public static void DisplayBonusesAndMaluses(Stats stats)
        {
            List<(string stat, double value)> bonuses = new List<(string stat, double value)>();
            List<(string stat, double value)> maluses = new List<(string stat, double value)>();

            List<string> statNames = new List<string>()
            {
                "Health",
                "Mana",
                "Strength",
                "Resistance",
                "MagicalMight",
                "MagicalMending",
                "Agility",
                "Deftness"
            };

            if (stats != null)
            {
                statNames.ForEach(statName => BonusOrMalus(stats, statName, bonuses, maluses));

                if (bonuses.Count > 0)
                {
                    Utils.Endl();
                    Utils.Cconsole.Absolute().Offset(35).Green.WriteLine("Bonus:");
                    bonuses.ForEach(b => Utils.Cconsole.Absolute().Offset(37).WriteLine("+{0} {1}", b.value, b.stat));
                }

                if (maluses.Count > 0)
                {
                    Utils.Endl();
                    Utils.Cconsole.Absolute().Offset(35).Red.WriteLine("Malus:");
                    maluses.ForEach(b => Utils.Cconsole.Absolute().Offset(37).WriteLine("{0} {1}", b.value, b.stat));
                }
            }
        }

        public static void BonusOrMalus(
            Stats stats,
            string stat,
            List<(string stat, double value)> bonuses,
            List<(string stat, double value)> maluses,
            string statName = null
        )
        {
            double statValue = stats[stat];
            if (statValue > 0)
            {
                bonuses.Add((stat: statName ?? stat, value: statValue));
            }
            else if (statValue < 0)
            {
                maluses.Add((stat: statName ?? stat, value: statValue));
            }
        }
    }
}
