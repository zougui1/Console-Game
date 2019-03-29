using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;

namespace ConsoleGame.items.stuff
{
    public abstract class AbstractEquipment : Item
    {
        /// <summary>
        /// Stats represent the additional stats given by the equipment
        /// </summary>
        public Stats Stats { get; protected set; }
        public string Category { get; private set; }
        /// <summary>
        /// Equipable is used to know which classes can wear this armor
        /// </summary>
        public List<string> Equipable { get; private set; }

        public AbstractEquipment(string name, string description, int coins = 0) : base(name, description, coins)
        {
            Equipable = new List<string>();
        }

        public AbstractEquipment(string name, string description, int coins, Stats stats, string category, List<string> equipable)
            : base(name, description, coins)
        {
            Stats = stats;
            Category = category;
            Equipable = equipable;
        }
    }
}