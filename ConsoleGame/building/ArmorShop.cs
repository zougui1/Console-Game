using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;
using ConsoleGame.json;

namespace ConsoleGame.building
{
    public class ArmorShop : Shop
    {
        public ArmorMerchant Merchant { get; set; }

        public ArmorShop() : base()
        { }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }
    }
}
