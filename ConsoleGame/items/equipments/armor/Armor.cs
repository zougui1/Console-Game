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
    public class Armor : AbstractEquipment
    {
        /// <summary>
        /// Category represent the category of the armor (head, torso, arm, leg, feet)
        /// </summary>
        public string SubCategory { get; protected set; }
        public int Defense { get; protected set; }
        /// <summary>
        /// Equipable is used to know which classes can wear this armor
        /// </summary>
        public List<string> Equipable { get; protected set; }

        public Armor(string name, string description, int defense = 0) : base(name, description)
        {
            Defense = defense;
            Equipable = new List<string>();
        }
    }
}
