using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;
using ConsoleGame.utils;

namespace ConsoleGame.items.stuff.handed
{
    public abstract class AbstractHanded : AbstractEquipment
    {
        /// <summary>
        /// TwoHanded is used to know if the equipment need two hands to be used
        /// </summary>
        public bool TwoHanded { get; private set; }
        public int Damages { get; protected set; }

        public AbstractHanded(string name, string description, int coins = 0, int damages = 0) : base(name, description, coins)
        {
            Damages = damages;
            TwoHanded = false;
        }

        public AbstractHanded(
            string name, string description, int coins, Stats stats, string category, int damages, List<string> equipable
        ) : base(name, description, coins, stats, category, equipable)
        {
            Damages = damages;
            TwoHanded = false;
        }

        /*public void Display()
        {
            Console.WriteLine("Weapon: {0} (damages: {1})", Name, Damages);
        }*/

        /// <summary>
        /// Init is used to initialize the equipment
        /// - if the type of the equipment is a type that need two hands to be used, define the property TwoHanded to true
        /// </summary>
        public void Init()
        {
            if (Utils.InEnum(Category, typeof(TwoHanded)))
            {
                TwoHanded = true;
            }
        }
    }
}