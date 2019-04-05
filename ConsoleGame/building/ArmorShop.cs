using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.entity.NPC;
using ConsoleGame.game;
using ConsoleGame.json;

namespace ConsoleGame.building
{
    public class ArmorShop : Shop
    {
        public ArmorMerchant Merchant { get; private set; }

        [JsonConstructor]
        public ArmorShop(List<AbstractNPC> npcs, bool isLocked, string category, ArmorMerchant merchant) : base(npcs, isLocked, category)
        {
            Merchant = merchant;
        }

        public void DisplayList()
        {
            Merchant.DisplayList();
        }

        public override void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}
