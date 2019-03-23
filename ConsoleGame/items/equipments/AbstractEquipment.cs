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
        public Stats Stats { get; set; }
        public string Category { get; set; }

        public AbstractEquipment(string name, string description) : base(name, description)
        {
            Stats = new Stats();
        }
    }
}