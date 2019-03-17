using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.location;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //LocationList test = new LocationList();
            //Json.DataDir();
            //Json.DataDir();
            //Json.GetWeapon(0);
            LocationList.SetLocations();
            Game.Init();
            Console.ReadKey();
            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app!
        }
    }
}
