using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.stats;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.misc;
using ConsoleGame.utils;

namespace ConsoleGame.entity
{
    public class Monster : Entity
    {
        /// <summary>
        /// Loots is an array of ValueTuple and represent the items the monster can loot
        /// - Percent is the percent of chance to drop the item
        /// - Id is the id of the item
        /// - DataPath is the path of the json where the item is
        /// </summary>
        public LootTable[] LootsTable { get; set; }

        public Monster(string name) : base(name)
        {

        }

        [JsonConstructor]
        public Monster(
            string name, EntityStats entityStats, Weapon weapon, List<Spell> spells,
            Shield shield, Armor head, Armor torso, Armor arms, Armor legs, Armor feet
        ) : base(name, entityStats, weapon, spells, shield, head, torso, arms, legs, feet)
        { }

        public List<Item> Loots()
        {
            List<Item> drops = new List<Item>();
            
            for(int i = 0; i < LootsTable.Length; ++i)
            {
                (double percent, int id, string dataType) = LootsTable[i];
                double random = RandomNumber.Between(0, 100) + new Random().NextDouble();
                
                if(random > 100)
                {
                    random = 100;
                }
                
                if(random <= percent)
                {
                    Item item = Json.GetRightItem(id, dataType);
                    drops.Add(item);
                    Utils.Cconsole.Color("Blue").WriteLine("{0} dropped a {1}", Name, item.Name);
                }
            }

            return drops;
        }
    }
}
