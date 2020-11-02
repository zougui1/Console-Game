using ConsoleGame.entity.stats;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ConsoleGame.items.stuff.handed.weapons
{
    public class Weapon : AbstractHanded
    {
        public Weapon(string name, string description, int coins = 0, int damages = 0) : base(name, description, coins, damages)
        { }

        [JsonConstructor]
        public Weapon(
            string name, string description, int coins, Stats stats, string category, int damages
        ) : base(name, description, coins, stats, category, damages)
        { }
    }
}
