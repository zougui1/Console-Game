using ConsoleGame.entity;
using ConsoleGame.misc.coords;
using ConsoleGame.misc.inventory;
using ConsoleGame.items;
using ConsoleGame.UI.header;
using ConsoleGame.UI.menus;
using ConsoleGame.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleGame.game
{
    public class User
    {
        public List<Character> Characters { get; set; }
        public MovableCoords Coords { get; set; }
        public List<Monster> MonstersInBattle { get; set; }
        public Inventory Bag { get; set; }
        public int Gold { get; set; } = 0;

        public User()
        {
            Coords = new MovableCoords();
            Characters = new List<Character>();
            Bag = new Inventory();
        }

        public User(Character character)
        {
            Coords = new MovableCoords();
            Characters = new List<Character>() { character };
            Bag = new Inventory();
            Bag.Add(new Item("Small potion", "description"));
            Bag.Add(new Item("Small potion", "description"));
            Bag.Add(new Item("Small potion", "description"));
            Bag.Add(new Item("Something else", "description"));
            Bag.Add(new Item("Small potion", "description"));
            Bag.Add(new Item("Something else", "description"));
            Bag.Add(new Item("Something else", "description"));
            Bag.Add(new Item("Something else", "description"));
            Bag.Add(new Item("Small potion", "description"));
            Bag.Add(new Item("nothing", "description"));
            Bag.Add(new Item("nothing", "description"));
            Bag.Add(new Item("nothing", "description"));
        }

        [JsonConstructor]
        public User(List<Character> characters, MovableCoords coords, int gold)
        {
            Characters = characters;
            Coords = coords;
            Gold = gold;
        }

        public void ChooseAction()
        {
            if (Console.CursorTop >= Console.WindowHeight - 15)
            {
                Utils.SetTimeoutSync(() => Console.Clear(), 100);
            }

            Header.Render();
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
                    case GameStatement.InBuilding:
                        Actions.InBuilding(this);
                        break;
                }
            }
        }

        public void ChooseDirection(object args)
        {
            Utils.Endl();
            TAction<Directions> method = new TAction<Directions>(Coords.NumberMove);

            Menu<TAction<Directions>, Directions> menu = new Menu<TAction<Directions>, Directions>("Where do you want to move?")
                .AddChoice("Up", method, Directions.Up)
                .AddChoice("Left", method, Directions.Left)
                .AddChoice("Down", method, Directions.Down)
                .AddChoice("Right", method, Directions.Right);
            menu.Kind = "UI";
            menu.SinglePage = true;
            menu.InitSelection();
        }

        public void Rest(object args = null)
        {
            Utils.Endl(2);
            Characters.ForEach(character =>
            {
                double maxHealth = character.EntityStats.MaxHealth;
                int healthPoints = RandomNumber.Between((int)maxHealth / 8, (int)maxHealth / 4);
                character.Regen(healthPoints);
                Utils.Cconsole.Green.WriteLine("After a little rest you recovered {0} health points", healthPoints);
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
            int totalGold = 0;

            MonstersInBattle.ForEach(monster =>
            {
                totalXP += monster.EntityStats.Experiences;
                totalGold += monster.Gold;
            });

            WinMessage(totalXP, totalGold);

            Gold += totalGold;
            Characters.ForEach(character => character.AddExperiencesIfAlive(totalXP));
        }

        public void WinMessage(int experiences, int gold)
        {
            Utils.Endl(2);
            if (MonstersInBattle.Count > 1)
            {
                Utils.Cconsole.Red.WriteLine("The ennemies has been defeated!");
            }
            else
            {
                Utils.Cconsole.Red.WriteLine("{0} has been defeated!", MonstersInBattle[0].Name);
            }

            Utils.Endl();
            if (Characters.Count > 1)
            {
                Utils.Cconsole.Green.WriteLine("The whole team has earned {0} experiences and {1} GP", experiences, gold);
            }
            else
            {
                Utils.Cconsole.Green.WriteLine("{0} has earned {1} experiences and {2} GP", Characters[0].Name, experiences, gold);
            }
            Utils.Endl(2);
        }

        public void LooseMessage()
        {
            Utils.Endl(2);
            Utils.Cconsole.Red.WriteLine("You died!");
            Utils.Endl(2);
        }

        public int CharactersAliveCount()
        {
            return Characters.Where(c => c.IsAlive()).ToList().Count;
        }

        public int MonstersAliveCount()
        {
            return MonstersInBattle.Where(m => m.IsAlive()).ToList().Count;
        }

        public Character FirstCharacterAlive()
        {
            return Characters.First(c => c.IsAlive());
        }

        public Monster FirstMonsterAlive()
        {
            return MonstersInBattle.First(c => c.IsAlive());
        }

        public List<Character> AllCharactersAlive()
        {
            return Characters.FindAll(c => c.IsAlive());
        }

        public List<Monster> AllMonstersAlive()
        {
            return MonstersInBattle.FindAll(m => m.IsAlive());
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
            MonstersInBattle.ForEach(entity => entity.Loots().ForEach(item => Bag.Items.Add(item)));
        }

        public void RemoveFocus()
        {
            MonstersInBattle.ForEach(entity => entity.Focus = null);
            Characters.ForEach(entity => entity.Focus = null);
        }
    }
}
