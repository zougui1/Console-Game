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
            //Json.DataDir(); // uncomment before prod compilation
            LocationList.SetLocations();
            Game.Init();
            Console.ReadKey();
        }
    }
}
