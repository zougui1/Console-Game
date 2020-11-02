using ConsoleGame.entity.stats;
using System.Collections.Generic;

namespace ConsoleGame.items.stuff.handed.shields
{
    public class Shield : AbstractHanded
    {
        public int Defense { get; set; }

        public Shield(string name, string description, int defense = 0, int damages = 0, int coins = 0)
            : base(name, description, coins, damages)
        {
            Defense = defense;
        }

        public Shield(
            string name, string description, int coins, Stats stats, string category, int damages, int defense
        ) : base(name, description, coins, stats, category, damages)
        {
            Defense = defense;
        }
    }
}
