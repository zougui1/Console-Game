using ConsoleGame.entity;
using ConsoleGame.entity.classes;
using ConsoleGame.json;
using ConsoleGame.UI.menus;
using ConsoleGame.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleGame.game
{
    public static class GameMenu
    {
        public static Game Game { get; private set; }
        public static BeginClasses Class { get; private set; }
        public static string Name { get; private set; }

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
            Menu<Action, object> menu = new Menu<Action, object>("What do you want to do?")
                .AddChoice("Start a new game", new TAction<object>(NewGame))
                .AddChoice("Load a game", new TAction<object>(Loader));
            menu.Choose();
        }

        /// <summary>
        /// Loader is used to load and define the User property from a json object
        /// </summary>
        /// <param name="args">is unused but obligatory due to be used with the delegation Action</param>
        public static void Loader(object args)
        {
            Game = Json.Load();

            if (Game?.User == null)
            {
                Utils.Endl();
                Utils.Cconsole.Color("DarkRed").WriteLine("You don't have any party saved.");
                Utils.Endl();
                MainMenu();
            }
            else
            {
                StartGame();
            }
        }

        /// <summary>
        /// NewGame is used to create a Character with a name they have entered and define it in the User property
        /// </summary>
        /// <param name="args">is unused but obligatory due to be used with the delegation Action</param>
        public static void NewGame(object args)
        {
            ChooseClass();
        }

        public static void ChooseClass()
        {
            Utils.Endl();

            List<string> classes = Enum.GetNames(typeof(BeginClasses)).ToList();

            List<TAction<BeginClasses>> methods = new List<TAction<BeginClasses>>();
            classes.ForEach(x => methods.Add(new TAction<BeginClasses>(ChooseName)));

            BeginClasses[] args = (BeginClasses[])Enum.GetValues(typeof(BeginClasses));

            Menu<TAction<BeginClasses>, BeginClasses> menu = new Menu<TAction<BeginClasses>, BeginClasses>("Choose a class")
                .AddChoices(classes)
                .AddActions(methods)
                .AddArgs(args.ToList());
            menu.Choose();
        }

        public static void ChooseName(BeginClasses args)
        {
            Class = args;

            Utils.Endl();
            Console.WriteLine("Enter a name.");
            bool validName = false;

            while (!validName)
            {
                Name = Console.ReadLine();

                if (Name.Trim() != "")
                {
                    validName = true;
                    break;
                }
                Utils.Cconsole.Color("DarkRed").WriteLine("Enter a valid name.");
            }

            CreateParty();
        }

        public static void CreateParty()
        {
            Character character = new Character(Name, Class, Json.GetWeapon(0));
            User user = new User(character);
            Game = new Game(user);
            StartGame();
        }

        public static void StartGame()
        {
            Utils.Cconsole.Color("Green").WriteLine("The game is starting...");
            Thread.Sleep(1000);
            Console.Clear();

            Game.Start();
        }
    }
}
