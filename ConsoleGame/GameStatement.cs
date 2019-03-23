using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity;
using ConsoleGame.entity.classes;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame
{
    public static class GameStatement
    {
        public static Game Game { get; private set; }

        public static void Init()
        {
            Welcome();
            MainMenu();
        }

        public static void Welcome()
        {
            Console.WriteLine("Welcome in the Console Game!");
            Utils.Endl();
        }

        /// <summary>
        /// MainMenu is to display the menu before the game start (new game or load a game)
        /// </summary>
        public static void MainMenu()
        {
            Utils.Cconsole.Color("DarkGray").WriteLine("What do you want to do?");

            string[] choices = { "Start a new game", "Load a game" };
            Action[] methods = { new Action(NewGame), new Action(Loader) };
            Utils.Choices(choices, methods);
        }

        /// <summary>
        /// Loader is used to load and define the User property from a json object
        /// </summary>
        /// <param name="args">is unused but obligatory due to be used with the delegation Action</param>
        public static void Loader(object[] args)
        {
            Game = Json.Load();

            if(Game?.User == null)
            {
                Utils.Endl();
                Utils.Cconsole.Color("DarkRed").WriteLine("You don't have any party saved.");
                Utils.Endl();
                MainMenu();
            }
            else
            {
                Game.ChooseAction();
            }
        }

        /// <summary>
        /// NewGame is used to create a Character with a name they have entered and define it in the User property
        /// </summary>
        /// <param name="args">is unused but obligatory due to be used with the delegation Action</param>
        public static void NewGame(object[] args)
        {
            Console.WriteLine("Enter a name.");
            string name = "";
            bool validName = false;

            while (!validName)
            {
                name = Console.ReadLine();

                if (name.Trim() != "")
                {
                    validName = true;
                    break;
                }
                Utils.Cconsole.Color("DarkRed").WriteLine("Enter a valid name.");
            }
            
            Game = new Game(new Character(name, ((Classes)0).ToString(), Json.GetWeapon(0)));
            Game.ChooseAction();
        }
    }
}
