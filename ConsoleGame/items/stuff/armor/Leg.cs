using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items.stuff.armor
{
    public class Leg : Armor
    {
        public Leg(string name, string description, int defense = 0) : base(name, description, defense)
        {
            Category = "Legging";
        }
    }
}
