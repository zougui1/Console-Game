using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * puts the datas in a json file, the Equipable in an array and delete all the derivated armors
 */

namespace ConsoleGame.items.stuff.armor
{
    public class Armor : AbstractStuff
    {
        public string Category { get; protected set; }
        public int Defense { get; protected set; }
        public List<string> Equipable { get; protected set; }

        public Armor(string name, string description, int defense = 0) : base(name, description)
        {
            Defense = defense;
            Equipable = new List<string>();
        }
    }
}
