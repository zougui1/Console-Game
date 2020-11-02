using ConsoleGame.game;
using ConsoleGame.json;
using ConsoleGame.location;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.misc.inventory;
using ConsoleGame.UI;
using ConsoleGame.UI.menus;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ConsoleGame.utils;

namespace ConsoleGame
{
    // related to Menu
    public delegate void NullAction(object args);
    // related to Pagination
    public delegate void PaginateAction(int min, int max);
    public delegate void TAction<T>(T arg);
    public delegate void DefaultKeyPress(ConsoleKeyInfo key);
    // related to Listing
    public delegate void InitListing<TList>(TList element, int cursorPosition);
    public delegate void ItemListing<TList>(TList element);
    // related to SelectionList
    public delegate void LineChangedHandler(int currentCursorTop);

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("get armor based on ID from json doesn't work");
            System.Threading.Thread.Sleep(2000);
            /*
             * interface info: Item/
            ItemInfo itemInfo = new ItemInfo()
            {
                Item = new Item("Item name", "Item description")
                {
                    IsHeal = true,
                    Restore = 10
                }
            };
           /*/

            /*
             * interface info: Armor/
            ItemInfo itemInfo = new ItemInfo()
            {
                Item = new Armor("Item name", "Item description")
                {
                    Defense = 45,
                    Category = "Torso",
                    SubCategory = "Chestplate",
                    Stats = new entity.stats.Stats()
                    {
                        Health = 20,
                        Strength = 8,
                        Agility = -3,
                        Deftness = -7
                    }
                }
            };
            /*/

            /*
             * interface info: Shield/
            ItemInfo itemInfo = new ItemInfo()
            {
                Item = new Shield("Item name", "Item description")
                {
                    Damages = 15,
                    Defense = 30,
                    Category = "Shield",
                    SubCategory = "GreatShield",
                    Stats = new entity.stats.Stats()
                    {
                        Health = 20,
                        Strength = 8,
                        Agility = -3,
                        Deftness = -7
                    },
                    TwoHanded = true
                }
            };
            /*/

            /*
             * interface info: Weapon*/
            ItemInfo itemInfo = new ItemInfo()
            {
                Item = new Weapon("Item name", "Item description")
                {
                    Damages = 15,
                    Category = "Sword",
                    SubCategory = "GreatSword",
                    Stats = new entity.stats.Stats()
                    {
                        Health = 20,
                        Strength = 8,
                        Agility = -3,
                        Deftness = -7
                    },
                    TwoHanded = true
                }
            };
            /**/

            //itemInfo.Display();

            Inventory inventory = new Inventory();
            inventory.Add(new Item("Small potion", "description"));
            inventory.Add(new Item("Small potion", "description"));
            inventory.Add(new Item("Small potion", "description"));
            inventory.Add(new Item("Something else", "description"));
            inventory.Add(new Item("Small potion", "description"));
            inventory.Add(new Item("Something else", "description"));
            inventory.Add(new Item("Something else", "description"));
            inventory.Add(new Item("Something else", "description"));
            inventory.Add(new Item("Small potion", "description"));
            inventory.Add(new Item("nothing", "description"));
            inventory.Add(new Item("nothing", "description"));
            inventory.Add(new Item("nothing", "description"));
            inventory.Add(new Item("test_1", "description"));
            inventory.Add(new Item("test_2", "description"));
            inventory.Add(new Item("test_3", "description"));
            inventory.Add(new Item("test_4", "description"));
            inventory.Add(new Item("test_5", "description"));
            inventory.Add(new Item("test_6", "description"));
            inventory.Add(new Item("test_7", "description"));
            inventory.Add(new Item("test_8", "description"));
            inventory.Add(new Item("test_9", "description"));
            //inventory.Display();
            //Utils.Cconsole.BgMagenta.Absolute().Left(10).Write("test");
            /*List<(string name, int value, int amount)> list = new List<(string name, int value, int amount)>()
            {
                ("just a name", 0, 8),
                ("an item", 0, 4),
                ("another item", 0, 3),
                ("something else", 0, 1)
            };

            for (int j = 0; j < Console.WindowWidth; j++)
            {
                if ((Console.WindowWidth / 35 * 100) == j)
                {
                    Console.Write('+');
                }
                else
                {
                    Console.Write('-');
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                (string name, int value, int amount) = list[i];
                Utils.Cconsole
                    .Write(name)
                    .Absolute().Left().Offset(35).Write('|')
                    .Absolute().Left().Offset(45).Write(value)
                    .Absolute().Left().Offset(55).Write('|')
                    .Absolute().Left().Offset(70).Write(amount);
                Utils.Endl();
            }
            for (int j = 0; j < Console.WindowWidth; j++)
            {
                if ((Console.WindowWidth / 35 * 100) == j)
                {
                    Console.Write('+');
                }
                else
                {
                    Console.Write('-');
                }
            }

            Console.ReadKey();*/
            Json.Init();
            LocationList.SetLocations();
            GameMenu.Init();

            Console.ReadKey();
        }
    }
}



