using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class WeaponShop : Shop
    {
        public WeaponMerchant Merchant { get; set; }

        public WeaponShop() : base()
        { }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }
    }
}
