using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.NPC;

namespace ConsoleGame.building
{
    public class WeaponShop : Shop
    {
        public WeaponMerchant Merchant { get; private set; }

        [JsonConstructor]
        public WeaponShop(List<AbstractNPC> npcs, bool isLocked, string category, WeaponMerchant merchant) : base(npcs, isLocked, category)
        {
            Merchant = merchant;
        }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }
    }
}
