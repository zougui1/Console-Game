using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.entity;
using ConsoleGame.entity.classes;
using ConsoleGame.location;
using ConsoleGame.location.locationTypes;

namespace ConsoleGame
{
    public delegate void Action(object[] args);

    class Game
    {
        public static Character User { get; set; }
        public static string CurrentLocationType { get; set; }
        public static Town CurrentTown { get; set; }
        public static City CurrentCity { get; set; }
        public static Kingdom CurrentKingdom { get; set; }


        private static int s_percentOfMonster = 10;
        public static int PercentOfMonster
        {
            get { return s_percentOfMonster; }
            set { s_percentOfMonster = value; }
        }

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

        public static void MainMenu()
        {
            Utils.Cconsole.Color("DarkGray").WriteLine("What do you want to do?");

            string[] choices = { "Start a new game", "Load a game" };
            Action[] methods = { new Action(NewGame), new Action(Loader) };
            Utils.Choices(choices, methods);
        }

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

        public static void ChooseAction()
        {
            Utils.Endl();
            Utils.Cconsole.Color("DarkGray").WriteLine("What do you want to do?");

            string[] actions = { "Move", "Rest" };
            Action[] methods = { new Action(User.ChooseDirection), new Action(User.Rest) };
            Utils.Choices(actions, methods);
        }

        public static void Moves(int movements)
        {
            for(int i = 0; i < movements; ++i)
            {
                if (MoveOnce())
                {
                    return;
                }
            }
            Console.WriteLine("Nothing happened.");
            Utils.Endl();
            ChooseAction();
        }

        static private bool MoveOnce()
        {
            int number = RandomNumber.Between(0, 100);
            if(number <= s_percentOfMonster)
            {
                TriggerMonster();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void TriggerMonster()
        {
            Monster monster = new Monster("Slime");
            monster.Focus = User;
            Utils.Endl();
            Utils.Endl();
            Console.WriteLine("A {0} appears", monster.Name);
            User.Focus = monster;
            Battle(monster);
        }

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

        public static void InCity()
        {
            switch (CurrentLocationType)
            {
                case "City":
                    CurrentCity.Display();
                    break;
                default: break;
            }
        }
    }
}
