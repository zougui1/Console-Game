using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items.stuff.armor
{
    public class Arm : Armor
    {
        public Arm(string name, string description, int defense = 0) : base(name, description, defense)
        {
            Category = "Arm";
        }
    }
}
