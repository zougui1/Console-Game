using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.misc
{
    public class Spell
    {
        public string Name { get; protected set; }
        public int RequiredMana { get; protected set; }
        public int Power { get; protected set; }
        public string Type { get; protected set; }

        public Spell(string name, int requiredMana, int power, string type)
        {
            Name = name;
            RequiredMana = requiredMana;
            Power = power;
            Type = type;
        }
    }
}
