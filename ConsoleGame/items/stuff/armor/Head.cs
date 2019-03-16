using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.entity.stats;

namespace ConsoleGame.items.stuff.armor
{
    public class Head : Armor
    {
        public Head(string name, string description, int defense = 0) : base(name, description, defense)
        {
            Category = "Head";
        }
    }
}
