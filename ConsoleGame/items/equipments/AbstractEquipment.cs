using ConsoleGame.entity.stats;
using System.Collections.Generic;

namespace ConsoleGame.items.stuff
{
    public abstract class AbstractEquipment : Item
    {
        /// <summary>
        /// Stats represent the additional stats given by the equipment
        /// </summary>
        public Stats Stats { get; set; }
        public string Category { get; set; }
        /// <summary>
        /// SubCategory represent the kind of armor (chestplate, robe, costume, etc...)
        /// </summary>
        public string SubCategory { get; set; }
        /// <summary>
        /// Equipable is used to know which classes can wear this armor
        /// </summary>
        public List<string> Equipable { get; set; }

        public AbstractEquipment(string name, string description, int coins = 0) : base(name, description, coins)
        {
            Equipable = new List<string>();
        }

        public AbstractEquipment(string name, string description, int coins, Stats stats, string category)
            : base(name, description, coins)
        {
            Stats = stats;
            Category = category;
        }
    }
}