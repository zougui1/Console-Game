using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.NPC;
using ConsoleGame.game;

namespace ConsoleGame.building
{
    public class ItemShop : Shop
    {
        public ItemMerchant ItemMerchant { get; private set; }

        [JsonConstructor]
        public ItemShop(Citizen[] citizens, bool isLocked, string category, ItemMerchant itemMerchant) : base(citizens, isLocked, category)
        {
            ItemMerchant = itemMerchant;
        }

        public void DisplayList(object arg = null)
        {
            ItemMerchant.DisplayList();
        }

        public override void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}
