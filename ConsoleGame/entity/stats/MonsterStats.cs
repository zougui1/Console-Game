using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.items.stuff;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;

namespace ConsoleGame.entity.stats
{
    public class MonsterStats : EntityStats
    {
        public MonsterStats(string name, out Weapon weapon, out (double percent, int id, string dataType)[] lootTable)
        {
            weapon = Json.GetWeapon(0);
            lootTable = new (double percent, int id, string dataType)[0];
            /*EntityStats monster = Json.GetMonster(name, out weapon, out lootTable);
            
            MaxHealth = monster.Health;
            Health = monster.Health;
            MaxMana = monster.Mana;
            Mana = monster.Mana;
            Strength = monster.Strength;
            Resistance = monster.Resistance;
            MagicalMight = monster.MagicalMight;
            MagicalMending = monster.MagicalMending;
            Agility = monster.Agility;
            Deftness = monster.Deftness;
            Experiences = monster.Experiences;*/
        }
    }
}
