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
using ConsoleGame.items.stuff.handed.weapons;

namespace ConsoleGame
{
    public delegate void Action(object[] args);

    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Json.Init();
            LocationList.SetLocations();
            GameMenu.Init();
            Console.ReadKey();
        }
    }
}



