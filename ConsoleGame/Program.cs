using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.json;
using ConsoleGame.location;

namespace ConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Json.DataDir(); // uncomment before prod compilation
            Json.Init();
            LocationList.SetLocations();
            Game.Init();
            Console.ReadKey();
        }

    }
}
