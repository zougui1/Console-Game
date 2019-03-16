using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleGame.entity.stats;
using ConsoleGame.entity.managers;
using ConsoleGame.misc;
using ConsoleGame.misc.coords;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.items.stuff.armor;

namespace ConsoleGame.entity
{
    public class Character : Entity
    {
        public int Potions { get; protected set; }
        public int NeededExperiences { get; protected set; }
        public string Class { get; protected set; }
        protected LevelingManager LevelingManager { get; set; }
        public string UpdatedStats { get; set; }
        public bool HasSpells { get; protected set; }
        public MovableCoords Coords { get; protected set; }

        public Character(string name, string className, Weapon weapon) : base(name, weapon)
        {
            Potions = 2;
            NeededExperiences = 14;
            Class = className;
            LevelingManager = new LevelingManager(this);
            HasSpells = false;
            Coords = new MovableCoords();
            InitStats initStats = Json.GetInitStats(className);

            initStats.Init();
            EntityStats = initStats;
        }
        
        public void ChooseAction()
        {
            Console.WriteLine(Environment.NewLine);
            int i = 0;
            Utils.Cconsole.Color("DarkGray").WriteLine("Do an action:");

            Entity[] target = { Focus };
            string[] actions = new string[4] { "Attack", "Defend", null, null };
            Action[] methods = new Action[4] { new Action(Attack), new Action(Defending), new Action(DrinkHealthPotion), new Action(ChooseSpell) };
            object[][] args = { target, null, null, target };

            if (Potions > 0)
            {
                actions[2] = $"Drink a potion ({Potions})";
            }

            if (HasSpells)
            {
                actions[3] = $"Spells (mana: {EntityStats.Mana}/{EntityStats.MaxMana})";
            }

            Utils.Choices(actions, methods, args);
        }

        public void ChooseSpell(object[] args)
        {
            Entity[] target = (Entity[])args[0];
            Action method = new Action(Magical);

            string[] actions = ListSpells();
            Action[] methods = { method, method, method, method, method, method, method, method, method, method };
            object[][] allArgs = { target, target, target, target, target, target, target, target, target, target };

            Utils.Choices(actions, methods, allArgs, parameter: Spells);
        }

        public void Magical(object[] args)
        {
            /*
             * the character cast it no matter how many mana it has, fix it
             */
            Entity target = ((Entity[])args[0])[0];
            Spell spell = (Spell)args[1];

            if (EntityStats.Mana < spell.RequiredMana)
            {
                Utils.Endl();
                Utils.Cconsole.Color("DarkRed").WriteLine("You don't have enough mana to cast \"{0}\"", spell.Name);
                ChooseAction();
                return;
            }

            EntityStats.Mana -= spell.RequiredMana;

            double damages = EntityStats.MagicalMight + (spell.Power * 0.95) - (int)(target.EntityStats.Resistance + (target.GetTotalDefense() * 0.8));
            InflictDamage(target, damages, spell);
        }

        public string[] ListSpells()
        {
            string[] spellList = new string[Spells.Count];
            int index = 0;

            for(int i = 0; i < Spells.Count; ++i)
            {
                Spell spell = Spells[i];

                if(spell == null)
                {
                    continue;
                }
                string spellStr = "";

                spellStr += $"{spell.Name}: ";
                spellStr += $"power: {spell.Power}, ";
                spellStr += $"required mana: {spell.RequiredMana}, ";
                spellList[index++] = spellStr;
            }

            return spellList;
        }

        public void DrinkHealthPotion(object[] args)
        {
            if (Potions > 0)
            {
                --Potions;
                InstantHealth(8);
                Utils.Cconsole.Color("Green").WriteLine("{0} drink a potion and has now {1} health points.", Name, EntityStats.Health);
                Utils.Cconsole.WriteLine("{0} has {1} potions left", Name, Potions);
            }
        }

        public void ChooseDirection(object[] args)
        {
            Utils.Endl();
            Action method = new Action(Coords.NumberMove);
            string[] actions = { "Up", "Left", "Down", "Right" };
            Action[] methods = { method, method, method, method };
            object[][] actionArgs = {
                new object[] { Directions.Up },
                new object[] { Directions.Left },
                new object[] { Directions.Down },
                new object[] { Directions.Right }
            };
            Utils.Choices(actions, methods, actionArgs);
        }

        public void GetAllStats()
        {
            EntityStats warrior = new EntityStats();
            PropertyInfo[] warriorStats = Utils.GetProperties(warrior);
            Type type = EntityStats.GetType();

            for(int i = 0; i < warriorStats.Length; ++i)
            {
                PropertyInfo property = warriorStats[i];
                string statName = property.Name;

                if (statName == "Branch" || statName == "MaxHealth" || statName == "MaxMana")
                {
                    continue;
                }

                PropertyInfo propertyInfo = type.GetProperty(statName);
                Console.Write(statName.PadRight(20));

                if(statName == "Experiences")
                {
                    Console.Write("  ");
                }
                else if(statName == "Level")
                {
                    int previousLevel = EntityStats.Level - 1;
                    if(previousLevel < 10)
                    {
                        Console.Write(" {0}  ".PadRight(3), previousLevel);
                    }
                    else
                    {
                        Console.Write("{0}  ".PadRight(3), previousLevel);
                    }
                    Console.Write("->".PadRight(5));
                }

                if(UpdatedStats.IndexOf(statName) >= 0)
                {
                    Regex regex = new Regex(statName + ":" + "[0-9]+,");
                    string match = regex.Match(UpdatedStats).ToString();
                    string addedNumber = match.Split(':')[1].Split(',')[0];
                    int matchInt = int.Parse(addedNumber);

                    Utils.Cconsole.Color((matchInt > 0) ? "Green" : "White").Write("+{0}", matchInt);
                    //Console.Write("+{0}", match.Split(':')[1].Split(',')[0]);
                }

                if (statName == "Level")
                {
                    Console.Write("{0}", EntityStats.Level);
                }
                else
                {
                    Console.Write("{0}".PadLeft(10), propertyInfo.GetValue(EntityStats));
                }

                if(statName == "Experiences")
                {
                    Console.Write("/{0}", NeededExperiences);
                }
                else if (statName == "Health")
                {
                    Console.Write("/{0}", EntityStats.MaxHealth);
                }
                else if (statName == "Mana")
                {
                    Console.Write("/{0}", EntityStats.MaxMana);
                }

                Utils.Endl();
            }
            Utils.Endl();
        }

        public void AddExperiences()
        {
            EntityStats.Experiences += Focus.EntityStats.Experiences;
            if(EntityStats.Experiences >= NeededExperiences)
            {
                EntityStats.Experiences -= NeededExperiences;
                NeededExperiences *= 2;
                Utils.Endl(2);
                Utils.Cconsole.Color("Green").WriteLine("{0} has level up", Name);
                LevelingManager.LevelUp();
                Utils.Endl();
                GetAllStats();
            }
        }

        public void Win()
        {
            WinMessage();
            AddExperiences();
        }

        public void WinMessage()
        {
            Utils.Endl(2);
            Utils.Cconsole.Color("Red").WriteLine("{0} died, {1} won!", Focus.Name, Name);
            Utils.Endl();
            Utils.Cconsole.Color("Cyan").WriteLine("-----------------------------");
            Utils.Cconsole.Color("Blue").WriteLine("name: {0}", Name);
            Utils.Cconsole.Color("Blue").WriteLine("health: {0}/{1}", EntityStats.Health, EntityStats.MaxHealth);
            Utils.Cconsole.Color("Blue").WriteLine("XP: {0}/{1}", EntityStats.Experiences + Focus.EntityStats.Experiences, NeededExperiences);
            Utils.Cconsole.Color("Blue").WriteLine("weapon: {0} ({1} damages)", Weapon.Name, Weapon.Damages);
            Utils.Cconsole.Color("Cyan").WriteLine("-----------------------------");
            Utils.Endl(2);
        }

        public void LooseMessage()
        {
            Utils.Endl(2);
            Utils.Cconsole.Color("Red").WriteLine("You died!");
            Utils.Endl(2);
        }
    }
}
