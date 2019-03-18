using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.items;

namespace ConsoleGame.items.stuff.handed.weapons
{
    public class Weapon : AbstractHanded
    {
        public Weapon(string name, string description, int damages = 0) : base(name, description)
        {
            Damages = damages;
        }
    }
}
