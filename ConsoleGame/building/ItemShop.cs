using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class ItemShop : Shop
    {
        public ItemMerchant Merchant { get; set; } 

        public ItemShop() : base()
        { }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }
    }
}
