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

namespace ConsoleGame.game
{
    public class Game
    {
        /// <summary>
        /// The User property represent the actual user of the game
        /// </summary>
        public User User { get; private set; }
        /// <summary>
        /// The CurrentLocation property represent the the current location 
        /// </summary>
        public Location CurrentLocation { get; set; }
        public GameStatement Statement { get; set; }
        /// <summary>
        /// The PercentOfMonster property represent the percent of chance to meet a monster at each movement
        /// </summary>
        public int PercentOfMonster { get; set; } = 90;

        public Game(User user)
        {
            User = user;
            Statement = GameStatement.Wilderness;
        }

        public void Start()
        {
            User.ChooseAction();
        }

        /// <summary>
        /// TriggerMonster is used to create a monster and set the user's focus the newly created monster when the user has encountered a monster
        /// </summary>
        public void TriggerMonster()
        {
            //Monster monster = new Monster("Slime");
            Monster monster = Json.GetMonster(0);
            monster.Focus = User.Characters[0];
            User.Characters[0].Focus = monster;
            Utils.Endl(2);
            Console.WriteLine("A {0} appears", monster.Name);
            User.MonstersInBattle = new List<Monster>() { monster };

            Utils.SetTimeoutSync(() =>
            {
                Console.Clear();
            }, 1000);
            
            Battle();
        }

        /// <summary>
        /// Battle is used for the battles between the user and the monsters
        /// if the user win it display a win message and save the party into a json file (the save shouldn't be here)
        /// if the user loose it displays a loose message and define the User property to null
        /// </summary>
        public void Battle()
        {
            Statement = GameStatement.Battle;
            List<Entity> battleOrder = new List<Entity>();
            // we add all the characters and all the monsters into the battleOrder list
            battleOrder.AddRange(User.Characters);
            battleOrder.AddRange(User.MonstersInBattle);
            // we order by desc the list depending on the agility, the entity with the most agility can do an action first
            battleOrder = battleOrder.OrderByDescending(entity => entity.EntityStats.Agility).ToList();

            while (User.IsTeamAlive() && User.IsEnemyAlive())
            {
                battleOrder.ForEach(entity =>
                {
                    if (entity.IsAlive())
                    {
                        entity.ChooseAction();
                    }
                });
                Utils.Endl();
                Utils.FillLine('-');
            }
            
            if (User.BattleEnd()){
                Json.Save(this);
            }
            else
            {
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
