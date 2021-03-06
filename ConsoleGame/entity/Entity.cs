﻿using ConsoleGame.entity;
using ConsoleGame.entity.stats;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.misc;
using ConsoleGame.utils;
using System;
using System.Collections.Generic;

namespace ConsoleGame
{
    public class Entity
    {
        public string Name { get; protected set; }
        public EntityStats EntityStats { get; protected set; }
        public bool Defend { get; protected set; }
        public Weapon Weapon { get; protected set; }
        public Entity Focus { get; set; }
        public double DodgeChancePerUnit { get; private set; }
        public double CriticalChancePerUnit { get; private set; }
        public List<Spell> Spells { get; protected set; }
        public Shield Shield { get; set; }
        public Armor Head { get; set; }
        public Armor Torso { get; set; }
        public Armor Arms { get; set; }
        public Armor Legs { get; set; }
        public Armor Feet { get; set; }

        private Entity()
        {
            Defend = false;
            DodgeChancePerUnit = 0.1;
            CriticalChancePerUnit = 0.15;
        }

        public Entity(string name, Weapon weapon = null) : this()
        {
            Name = name;

            if (weapon != null)
            {
                weapon.Init();
            }

            Weapon = weapon;
        }

        public Entity(
            string name,
            Weapon weapon,
            List<Spell> spells,
            Shield shield,
            Armor head,
            Armor torso,
            Armor arms,
            Armor legs,
            Armor feet
        ) : this(name, weapon)
        {
            Spells = spells ?? new List<Spell>();
            Shield = shield;
            Head = head;
            Torso = torso;
            Arms = arms;
            Legs = legs;
            Feet = feet;
        }

        public Entity(
            string name, InitStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        ) : this(name, weapon, spells, shield, head, torso, arms, legs, feet)
        {
            EntityStats = entityStats;
        }

        public Entity(
            string name, EntityStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        ) : this(name, weapon, spells, shield, head, torso, arms, legs, feet)
        {
            EntityStats = entityStats;
        }

        public bool IsAlive()
        {
            return EntityStats.Health > 0;
        }

        public virtual void ChooseAction()
        {
            Attack(Focus);
        }

        public int GetTotalDefense()
        {
            int defense = 0;

            if (Shield != null)
            {
                defense += Shield.Defense;
            }

            if (Head != null)
            {
                defense += Head.Defense;
            }

            if (Torso != null)
            {
                defense += Torso.Defense;
            }

            if (Arms != null)
            {
                defense += Arms.Defense;
            }

            if (Legs != null)
            {
                defense += Legs.Defense;
            }

            if (Feet != null)
            {
                defense += Feet.Defense;
            }
            return defense;
        }
        
        public int GetTotalStats(string statName)
        {
            double stat = EntityStats[statName];

            if (Weapon != null)
            {
                stat += Weapon?.Stats[statName] ?? 0;
            }

            if (Shield != null)
            {
                stat += Shield?.Stats[statName] ?? 0;
            }

            if (Head != null)
            {
                stat += Head?.Stats[statName] ?? 0;
            }

            if (Torso != null)
            {
                stat += Torso?.Stats[statName] ?? 0;
            }

            if (Arms != null)
            {
                stat += Arms?.Stats[statName] ?? 0;
            }

            if (Legs != null)
            {
                stat += Legs?.Stats[statName] ?? 0;
            }

            if (Feet != null)
            {
                stat += Feet?.Stats[statName] ?? 0;
            }

            return (int)stat;
        }

        public void Attack(Entity target)
        {
            Physical(target);
        }

        public void Attack(object[] args)
        {
            Physical((Entity)args[0]);
        }

        public double GetDamages(Entity target)
        {
            double weaponDamages = 0;
            if (Weapon != null)
            {
                weaponDamages = Weapon.Damages;
            }

            double damages = GetTotalStats("Strength") + (weaponDamages * 0.9) - (int)(target.GetTotalStats("Resistance") + (target.GetTotalDefense() * 0.9));
            return damages * DamagesRandomizer();
        }

        public double GetMagicalDamages(Spell spell, Entity target)
        {
            double damages = GetTotalStats("MagicalMight") + (spell.Power * 0.95) - (int)(target.GetTotalStats("Resistance") + (target.GetTotalDefense() * 0.8));
            return damages * DamagesRandomizer();
        }

        private double DamagesRandomizer()
        {
            Random rand = new Random();
            int randomInt = RandomNumber.Between(8, 12);
            double random = (randomInt / 10) + rand.NextDouble();
            return random;
        }

        public void Physical(Entity target)
        {
            double damages = GetDamages(target);
            InflictDamage(target, damages);
        }

        public void MagicalAttack(Entity target, Spell spell)
        {
            EntityStats.Mana -= spell.RequiredMana;
            double damages = GetMagicalDamages(spell, target);
            InflictDamage(target, damages, spell);
        }

        public void InflictDamage(Entity target, double damages, Spell spell = null)
        {
            bool isCritical = false;

            if (target.Defend)
            {
                damages *= 0.75;
            }

            int dodgeChance = RandomNumber.Between(0, 100);
            if (dodgeChance <= (target.EntityStats.Agility * DodgeChancePerUnit))
            {
                Dodge(target);
                return;
            }

            int criticalChance = RandomNumber.Between(0, 100);
            if (criticalChance <= (EntityStats.Deftness * CriticalChancePerUnit))
            {
                damages += 3;
                damages *= 1.7;
                isCritical = true;
            }

            if (damages < 1)
            {
                damages = 1;
            }

            target.ReceiveDamages((int)damages);
            AttackMessage(target, (int)damages, spell, isCritical);
        }

        public void AttackMessage(Entity target, int damages, Spell spell = null, bool isCritical = false)
        {
            if (spell != null)
            {
                MagicalMessage(target, damages, spell, isCritical);
            }
            else
            {
                PhysicalMessage(target, damages, isCritical);
            }
            Console.WriteLine("{0} has now {1} health points.", target.Name, target.EntityStats.Health);
        }

        public void MagicalMessage(Entity target, int damages, Spell spell, bool isCritical)
        {
            string message;

            if (isCritical)
            {
                message = "{0} cast {1} to {2} but it's a critical hit and inflict to them {3} damages.";
            }
            else
            {
                message = "{0} cast {1} to {2} and inflict {3} damages.";
            }

            Utils.Cconsole.Red.WriteLine(message, Name, spell.Name, target.Name, damages);
        }

        public void PhysicalMessage(Entity target, int damages, bool isCritical)
        {
            string message;

            if (isCritical)
            {
                message = "{0} attack {1} but it's a critical hit and inflict {2} damages.";
            }
            else
            {
                message = "{0} attack {1} and inflict {2} damages.";
            }

            Utils.Cconsole.Red.WriteLine(message, Name, target.Name, damages);
        }

        public void Dodge(Entity target)
        {
            Utils.Cconsole.Gray.WriteLine("{0} attack {1}\t", Name, target.Name);
            Utils.Cconsole.Cyan.WriteLine("But {0} dodge\t", target.Name);
        }

        public void ReceiveDamages(int damages)
        {
            EntityStats.Health -= damages;
            if (EntityStats.Health < 0)
            {
                EntityStats.Health = 0;
            }
        }

        public void Defending(Entity args = null)
        {
            Utils.Cconsole.DarkMagenta.WriteLine("{0} is defending", Name);
            Defend = true;
        }

        public double GetHealingPoints(Spell spell)
        {
            double damages = EntityStats.MagicalMending + (spell.Power * 0.95);
            return damages * DamagesRandomizer();
        }

        public void CastHealingSpell(Spell spell)
        {
            Regen((int)GetHealingPoints(spell));
        }

        public void Regen(int healthPoints)
        {
            EntityStats.Health += healthPoints;
            if (EntityStats.Health > EntityStats.MaxHealth)
            {
                EntityStats.Health = EntityStats.MaxHealth;
            }
        }
    }
}
