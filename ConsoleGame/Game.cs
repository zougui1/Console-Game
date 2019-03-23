using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity;
using ConsoleGame.json;
using ConsoleGame.location;
using ConsoleGame.utils;

namespace ConsoleGame
{
    public class Game
    {
        /// <summary>
        /// The User property represent the actual user of the game
        /// </summary>
        public Character User { get; set; }
        /// <summary>
        /// The CurrentLocation property represent the the current location 
        /// </summary>
        public Location CurrentLocation { get; set; }
        /// <summary>
        /// The PercentOfMonster property represent the percent of chance to meet a monster at each movement
        /// </summary>
        public int PercentOfMonster { get; set; } = 90;

        public Game(Character user)
        {
            User = user;
        }

        /// <summary>
        /// ChooseAction is used to let the user choose an action when in the nature
        /// (move, rest)
        /// </summary>
        public void ChooseAction()
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
        public void TriggerMonster()
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
        public void Battle(Monster monster)
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
                Json.Save(this);
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
        public void InLocation()
        {
            CurrentLocation.Display();
        }
    }
}
