using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity;
using ConsoleGame.entity.classes;
using ConsoleGame.json;
using ConsoleGame.location;
using ConsoleGame.utils;

namespace ConsoleGame
{
    public delegate void Action(object[] args);

    public class Game
    {
        /// <summary>
        /// The User property represent the actual user of the game
        /// </summary>
        public static Character User { get; set; }
        /// <summary>
        /// The CurrentLocation property represent the the current location 
        /// </summary>
        public static Location CurrentLocation { get; set; }
        /// <summary>
        /// The PercentOfMonster property represent the percent of chance to meet a monster at each movement
        /// </summary>
        public static int PercentOfMonster { get; set; } = 90;

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
            User = Json.Load();

            if(User == null || User.Name == null)
            {
                Utils.Endl();
                Utils.Cconsole.Color("DarkRed").WriteLine("You don't have any party saved.");
                Utils.Endl();
                MainMenu();
            }
            else
            {
                ChooseAction();
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

            Character character = new Character(name, ((Classes)0).ToString(), Json.GetWeapon(0));
            User = character;
            ChooseAction();
        }

        /// <summary>
        /// ChooseAction is used to let the user choose an action when in the nature
        /// (move, rest)
        /// </summary>
        public static void ChooseAction()
        {
            Utils.Endl();
            Utils.Cconsole.Color("DarkGray").WriteLine("What do you want to do?");

            string[] actions = { "Move", "Rest" };
            Action[] methods = { new Action(User.ChooseDirection), new Action(User.Rest) };
            Utils.Choices(actions, methods);
        }

        /// <summary>
        /// TriggerMonster is used to create a monster and set the user's focus the newly created monster when the user has encountered a monster
        /// </summary>
        public static void TriggerMonster()
        {
            //Monster monster = new Monster("Slime");
            Monster monster = Json.GetMonster(0);
            monster.Focus = User;
            Utils.Endl(2);
            Console.WriteLine("A {0} appears", monster.Name);
            User.Focus = monster;
            Battle(monster);
        }

        /// <summary>
        /// Battle is used for the battles between the user and the monsters
        /// if the user win it display a win message and save the party into a json file (the save shouldn't be here)
        /// if the user loose it displays a loose message and define the User property to null
        /// </summary>
        /// <param name="monster">The monster the user has to fight</param>
        public static void Battle(Monster monster)
        {
            while(User.IsAlive() && monster.IsAlive())
            {
                User.ChooseAction();
                if(monster.IsAlive())
                {
                    monster.Attack(User);
                }
            }
            
            if (User.IsAlive())
            {
                User.Win();
                monster.Loots();
                User.Focus = null;
                Json.Save(User);
                ChooseAction();
            }
            else if(monster.IsAlive())
            {
                User.LooseMessage();
                monster.Focus = null;
                User = null;
            }
        }

        /// <summary>
        /// InLocation is used to call the actions menu available in the location
        /// </summary>
        public static void InLocation()
        {
            CurrentLocation.Display();
        }
    }
}
