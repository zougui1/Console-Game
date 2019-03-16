using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items.stuff.armor
{
    public class Feet : Armor
    {
        public Feet(string name, string description, int defense = 0) : base(name, description, defense)
        {
            Category = "Feet";
        }
    }
}
