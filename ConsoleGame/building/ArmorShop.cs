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
        public ArmorMerchant ArmorMerchant { get; private set; }

        [JsonConstructor]
        public ArmorShop(Citizen[] citizens, bool isLocked, string category, ArmorMerchant armorMerchant) : base(citizens, isLocked, category)
        {
            ArmorMerchant = armorMerchant;
        }

        public void DisplayList(object arg = null)
        {
            ArmorMerchant.DisplayList();
        }

        public override void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}
