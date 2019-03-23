using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame.entity.NPC
{
    public class ArmorMerchant : AbstractNPC
    {
        public Armor[] Armors { get; set; }
        public Shield[] Shields { get; set; }

        public void DisplayList()
        {
            Utils.Endl();
            DisplayArmors();
            DisplayShields();
            Utils.Endl();
        }

        private void DisplayArmors()
        {
            for (int i = 0; i < Armors.Length; ++i)
            {
                Armor armor = Armors[i];

                Console.WriteLine("{0}:   {1}", i + 1, armor.Name);
            }
        }

        private void DisplayShields()
        {
            for (int i = 0; i < Shields.Length; ++i)
            {
                Shield shield = Shields[i];

                Console.WriteLine("{0}:   {1}", i + 1, shield.Name);
            }
        }
    }
}
