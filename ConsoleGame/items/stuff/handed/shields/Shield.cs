using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items.stuff.handed.shields
{
    public abstract class Shield : AbstractHanded
    {
        public int Defense { get; protected set; }

        public Shield(string name, string description, int defense = 0, int damages = 0) : base(name, description, damages)
        {
            Defense = defense;
        }
    }
}
