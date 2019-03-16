using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class ArmorShop : Building
    {
        public ArmorMerchant ArmorMerchant { get; set; }

        public ArmorShop() : base()
        { }

        public void DisplayList()
        {
            ArmorMerchant.DisplayList();
        }
    }
}
