using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class ArmorShop : Shop
    {
        public ArmorMerchant Merchant { get; set; }

        public ArmorShop() : base()
        { }

        public void init()
        {
            Merchant = (ArmorMerchant)Json.GetNPC(MerchantId);
        }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }
    }
}
