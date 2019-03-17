using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;

namespace ConsoleGame.items.stuff
{
    public abstract class AbstractStuff : Item
    {
        public Stats Stats { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }

        public AbstractStuff(string name, string description) : base(name, description)
        {
            Stats = new Stats();
        }
    }
}
