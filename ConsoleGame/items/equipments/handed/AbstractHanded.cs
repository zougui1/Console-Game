using ConsoleGame.entity.stats;
using ConsoleGame.utils;
using System.Collections.Generic;

namespace ConsoleGame.items.stuff.handed
{
    public abstract class AbstractHanded : AbstractEquipment
    {
        /// <summary>
        /// TwoHanded is used to know if the equipment need two hands to be used
        /// </summary>
        public bool TwoHanded { get; set; }
        public int Damages { get; set; }

        public AbstractHanded(string name, string description, int coins = 0, int damages = 0) : base(name, description, coins)
        {
            Damages = damages;
            TwoHanded = false;
        }

        public AbstractHanded(
            string name, string description, int coins, Stats stats, string category, int damages
        ) : base(name, description, coins, stats, category)
        {
            Damages = damages;
            TwoHanded = false;
        }

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