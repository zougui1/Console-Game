using ConsoleGame.entity.stats;
using Newtonsoft.Json;
using System.Collections.Generic;
/*
 * puts the datas in a json file, the Equipable in an array and delete all the derivated armors
 */

namespace ConsoleGame.items.stuff.armor
{
    public class Armor : AbstractEquipment
    {
        public int Defense { get; set; }

        public Armor(string name, string description, int defense = 0) : base(name, description)
        {
            Defense = defense;
        }

        [JsonConstructor]
        public Armor(
            string name, string description, int coins, Stats stats, string category,
            int defense, string subCategory
        ) : base(name, description, coins, stats, category)
        {
            SubCategory = subCategory;
            Defense = defense;
        }
    }
}
