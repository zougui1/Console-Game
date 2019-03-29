﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.classes;
using ConsoleGame.entity.managers;
using ConsoleGame.entity.stats;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.misc;
using ConsoleGame.misc.inventory;
using ConsoleGame.utils;

namespace ConsoleGame.entity
{
    public class Character : Entity
    {
        public int Potions { get; protected set; }
        public int NeededExperiences { get; protected set; }
        public Classes ClassName { get; protected set; }
        protected LevelingManager LevelingManager { get; set; }
        public string UpdatedStats { get; set; }
        public bool HasSpells { get; protected set; }
        public Inventory Inventory { get; protected set; }

        public Character(string name, BeginClasses className, Weapon weapon) : base(name, weapon)
        {
            Potions = 2;
            NeededExperiences = 14;
            ClassName = (Classes)className;
            LevelingManager = new LevelingManager(this);
            InitStats initStats = Json.GetInitStats(className.ToString());
            Inventory = new Inventory(16);

            initStats.Init();
            EntityStats = initStats;

            switch (className)
            {
                case BeginClasses.Mage:
                case BeginClasses.Priest:
                case BeginClasses.Thief:
                    HasSpells = true;
                    break;
                default:
                    HasSpells = false;
                    break;
            }
        }

        [JsonConstructor]
        public Character(
            string name, EntityStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet,
            int neededExperiences, byte className, bool hasSpells, Inventory inventory
        ) : base(name, entityStats, weapon, spells, shield, head, torso, arms, legs, feet)
        {
            NeededExperiences = neededExperiences;
            ClassName = (Classes)className;
            HasSpells = hasSpells;
            Inventory = inventory;

            LevelingManager = new LevelingManager(this);
        }

        public override void ChooseAction()
        {
            Utils.Endl(2);
            Utils.Cconsole.Color("DarkGray").WriteLine("Do an action:");

            Entity[] target = { Focus };
            string[] actions = new string[4] { "Attack", "Defend", null, null };
            Action[] methods = new Action[4] { new Action(Attack), new Action(Defending), new Action(DrinkHealthPotion), new Action(ChooseSpell) };
            object[][] args = { target, null, null, target };

            int lineNumber = 6;

            if (Potions > 0)
            {
                actions[2] = $"Drink a potion ({Potions})";
                ++lineNumber;
            }

            if (HasSpells)
            {
                actions[3] = $"Spells (mana: {EntityStats.Mana}/{EntityStats.MaxMana})";
                ++lineNumber;
            }

            Utils.Choices(actions, methods, args, removeLines: lineNumber);
        }

        public void ChooseSpell(object[] args)
        {
            if(Spells.Count == 0)
            {
                Utils.Cconsole.Color("Red").WriteLine("You do not have any spell.");
                ChooseAction();
                return;
            }

            Entity target = (Entity)args[0];
            Action method = new Action(Magical);

            string[] actions = ListSpells();
            Action[] methods = new Action[actions.Length];
            Utils.FillArray(methods, method);
            object[][] allArgs = new object[actions.Length][];
            Utils.FillArray(allArgs, new Entity[] { target }, true);

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
            UpdatedStats = "";
        }

        public void AddExperiencesIfAlive(int experiences)
        {
            if (IsAlive())
            {
                AddExperiences(experiences);
            }
        }

        public void AddExperiences(int experiences)
        {
            EntityStats.Experiences += experiences;
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
    }
}
