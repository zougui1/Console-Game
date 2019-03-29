using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.stats;
using ConsoleGame.utils;

namespace ConsoleGame.items.stuff.handed.weapons
{
    public class Weapon : AbstractHanded
    {
        public Weapon(string name, string description, int coins = 0, int damages = 0) : base(name, description, coins, damages)
        { }

        [JsonConstructor]
        public Weapon(
            string name, string description, int coins, Stats stats, string category, int damages, List<string> equipable
        ) : base(name, description, coins, stats, category, damages, equipable)
        { }



        public override void Display(string color = "White")
        {
            Utils.Cconsole.Color(color).Write(Name.PadRight(16));
            Utils.Cconsole.Color(color).Write(Category.PadRight(16));
            Utils.Cconsole.Color(color).Write("{0} damages".PadRight(20), Damages);
            Utils.Cconsole.Color(color).WriteLine("{0} coins", Coins);
        }
    }
}
