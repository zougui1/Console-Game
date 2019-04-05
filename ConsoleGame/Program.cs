using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using ConsoleGame.game;
using ConsoleGame.json;
using ConsoleGame.location;

using ConsoleGame.misc.inventory;
using ConsoleGame.misc.map;
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
    public delegate void LineChangedHandler(int currentCursorTop);
    public delegate void InitListing<TList>(TList element, int cursorPosition, string color);
    public delegate void ItemListing<TList>(TList element);

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            /*Inventory inv = new Inventory();
            inv.AddItem(50);
            inv.Display();*/
            
            Json.Init();
            LocationList.SetLocations();
            GameMenu.Init();
            Console.ReadKey();
        }
    }
}



