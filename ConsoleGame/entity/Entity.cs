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
        public Shield Shield { get; protected set; }
        public Armor Head { get; protected set; }
        public Armor Torso { get; protected set; }
        public Armor Arms { get; protected set; }
        public Armor Legs { get; protected set; }
        public Armor Feet { get; protected set; }

        public Entity(string name, Weapon weapon = null)
        {
            Name = name;
            Defend = false;

            if (weapon != null)
            {
                weapon.Init();
            }

            Weapon = weapon;
            DodgeChancePerUnit = 0.1;
            CriticalChancePerUnit = 0.15;
            Spells = new List<Spell>();
        }

        public Entity(
            string name, InitStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        )
        {
            Name = name;
            EntityStats = entityStats;
            Weapon = weapon;
            Spells = spells;
            Shield = shield;
            Head = head;
            Torso = torso;
            Arms = arms;
            Legs = legs;
            Feet = feet;
        }

        public Entity(
            string name, EntityStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        )
        {
            Name = name;
            EntityStats = entityStats;
            Weapon = weapon;
            Spells = spells;
            Shield = shield;
            Head = head;
            Torso = torso;
            Arms = arms;
            Legs = legs;
            Feet = feet;
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

            double damages = EntityStats.Strength + (weaponDamages * 0.9) - (int)(target.EntityStats.Resistance + (target.GetTotalDefense() * 0.9));
            return damages * DamagesRandomizer();
        }

        public double GetMagicalDamages(Spell spell, Entity target)
        {
            double damages = EntityStats.MagicalMight + (spell.Power * 0.95) - (int)(target.EntityStats.Resistance + (target.GetTotalDefense() * 0.8));
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

            Utils.Cconsole.Color("Red").WriteLine(message, Name, spell.Name, target.Name, damages);
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

            Utils.Cconsole.Color("Red").WriteLine(message, Name, target.Name, damages);
        }

        public void Dodge(Entity target)
        {
            Utils.Cconsole.Color("Grey").WriteLine("{0} attack {1}\t", Name, target.Name);
            Utils.Cconsole.Color("Cyan").WriteLine("But {0} dodge\t", target.Name);
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
            Utils.Cconsole.Color("DarkMagenta").WriteLine("{0} is defending", Name);
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
