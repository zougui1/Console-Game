using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity;
using ConsoleGame.items;
using ConsoleGame.misc.coords;
using ConsoleGame.misc.inventory;
using ConsoleGame.UI.menus;
using ConsoleGame.utils;

namespace ConsoleGame.game
{
    public class User
    {
        public List<Character> Characters { get; set; }
        public MovableCoords Coords { get; set; }
        public List<Monster> MonstersInBattle { get; set; }
        public Inventory Inventory { get; set; }

        public User()
        {
            Coords = new MovableCoords();
            Characters = new List<Character>();
            Inventory = new Inventory();
        }

        public User(Character character)
        {
            Coords = new MovableCoords();
            Characters = new List<Character>() { character };
            Inventory = new Inventory();
        }

        [JsonConstructor]
        public User(List<Character> characters, MovableCoords coords)
        {
            Characters = characters;
            Coords = coords;
        }

        public void ChooseAction()
        {
            if (IsTeamAlive())
            {
                switch (GameMenu.Game.Statement)
                {
                    case GameStatement.Wilderness:
                        Actions.Wilderness(this);
                        break;
                    case GameStatement.InLocation:
                        Actions.InLocation(this);
                        break;
                }
            }
        }
        
        public void ChooseDirection(object args)
        {
            Utils.Endl();
            TAction<Directions> method = new TAction<Directions>(Coords.NumberMove);

            Menu<TAction<Directions>, Directions> menu = new Menu<TAction<Directions>, Directions>("")
                .AddChoice("Up", method, Directions.Up)
                .AddChoice("Left", method, Directions.Left)
                .AddChoice("Down", method, Directions.Down)
                .AddChoice("Right", method, Directions.Right);
            menu.Choose();
        }

        public void Rest(object args = null)
        {
            Utils.Endl(2);
            Characters.ForEach(character =>
            {
                double maxHealth = character.EntityStats.MaxHealth;
                int healthPoints = RandomNumber.Between((int)maxHealth / 8, (int)maxHealth / 4);
                character.InstantHealth(healthPoints);
                Utils.Cconsole.Color("Green").WriteLine("After a little rest you recovered {0} health points", healthPoints);
                Utils.Cconsole.WriteLine("{0} has now {1} health points", character.Name, character.EntityStats.Health);
                Utils.Endl();
            });
            Utils.Endl(2);
        }

        public bool BattleEnd()
        {
            GameMenu.Game.Statement = GameStatement.Wilderness;
            bool gameContinue = false;

            RemoveFocus();
            if (IsTeamAlive())
            {
                Win();
                GetLoots();
                MonstersInBattle = null;
                gameContinue = true;
            }
            else if (IsEnemyAlive())
            {
                LooseMessage();
            }

            Utils.ConsoleClearer();

            return gameContinue;
        }

        public void Win()
        {
            int totalXP = 0;
            MonstersInBattle.ForEach(monster => totalXP += monster.EntityStats.Experiences);
            WinMessage(totalXP);
            Characters.ForEach(character => character.AddExperiencesIfAlive(totalXP));
        }

        public void WinMessage(int experiences)
        {
            Utils.Endl(2);
            if(MonstersInBattle.Count > 1)
            {
                Utils.Cconsole.Color("Red").WriteLine("The ennemies has been defeated!");
            }
            else
            {
                Utils.Cconsole.Color("Red").WriteLine("{0} has been defeated!", MonstersInBattle[0].Name);
            }

            Utils.Endl();
            if(Characters.Count > 1)
            {
                Utils.Cconsole.Color("Green").WriteLine("The whole team has earned {0} experiences", experiences);
            }
            else
            {
                Utils.Cconsole.Color("Green").WriteLine("{0} has earned {1} experiences", Characters[0].Name, experiences);
            }
            Utils.Endl(2);
        }

        public void LooseMessage()
        {
            Utils.Endl(2);
            Utils.Cconsole.Color("Red").WriteLine("You died!");
            Utils.Endl(2);
        }

        public bool IsTeamAlive()
        {
            return Characters.Any(entity => entity.IsAlive());
        }

        public bool IsEnemyAlive()
        {
            return MonstersInBattle.Any(entity => entity.IsAlive());
        }

        public void GetLoots()
        {
            MonstersInBattle.ForEach(entity => entity.Loots().ForEach(item => Inventory.Items.Add(item)));
        }

        public void RemoveFocus()
        {
            MonstersInBattle.ForEach(entity => entity.Focus = null);
            Characters.ForEach(entity => entity.Focus = null);
        }
    }
}
