using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.utils;
using Newtonsoft.Json;
using System;

namespace ConsoleGame.entity.NPC
{
    public class ArmorMerchant : AbstractNPC
    {
        public Armor[] Armors { get; private set; }
        public Shield[] Shields { get; private set; }

        [JsonConstructor]
        public ArmorMerchant(string name, string category, Armor[] armors, Shield[] shields) : base(name, category)
        {
            Armors = armors;
            Shields = shields;
        }

        public void DisplayList(object arg = null)
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
