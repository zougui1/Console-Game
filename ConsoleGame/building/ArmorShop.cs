using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.items.stuff.armor;

namespace ConsoleGame.building
{
    public class ArmorShop : Shop
    {
        public Armor[] Armors { get; set; }

        public ArmorShop() : base()
        {

        }

        public void DisplayList()
        {
            for (int i = 0; i < Armors.Length; ++i)
            {
                Armor armor = Armors[i];

                Console.WriteLine("{0}:   {1}, {2} damages", i, armor.Name, armor.Defense);
            }
        }
    }
}
