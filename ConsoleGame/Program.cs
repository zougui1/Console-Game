using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.json;
using ConsoleGame.location;

namespace ConsoleGame
{
    public delegate void Action(object[] args);

    class Program
    {
        static void Main(string[] args)
        {
            Json.Init();
            LocationList.SetLocations();
            GameStatement.Init();
            Console.ReadKey();

            // make a non-static class for the game statement, which will be used to save the party
        }

    }
}
