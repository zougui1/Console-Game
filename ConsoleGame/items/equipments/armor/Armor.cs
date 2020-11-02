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
        /// <summary>
        /// Category represent the category of the armor (head, torso, arm, leg, feet)
        /// </summary>
        public string SubCategory { get; private set; }
        public int Defense { get; protected set; }

        public Armor(string name, string description, int defense = 0) : base(name, description)
        {
            Defense = defense;
        }

        [JsonConstructor]
        public Armor(
            string name, string description, int coins, Stats stats, string category,
            int defense, string subCategory, List<string> equipable
        ) : base(name, description, coins, stats, category, equipable)
        {
            SubCategory = subCategory;
            Defense = defense;
        }
    }
}
