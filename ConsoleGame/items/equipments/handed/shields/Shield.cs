using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.utils;

namespace ConsoleGame.items.stuff.handed.shields
{
    public abstract class Shield : AbstractHanded
    {
        public int Defense { get; protected set; }

        public Shield(string name, string description, int defense = 0, int damages = 0, int coins = 0)
            : base(name, description, coins, damages)
        {
            Defense = defense;
        }

        public Shield(
            string name, string description, int coins, Stats stats, string category, int damages, int defense, List<string> equipable
        ) : base(name, description, coins, stats, category, damages, equipable)
        {
            Defense = defense;
        }
    }
}
