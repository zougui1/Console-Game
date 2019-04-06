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
    public class WeaponShop : Shop
    {
        public WeaponMerchant WeaponMerchant { get; private set; }

        [JsonConstructor]
        public WeaponShop(Citizen[] citizens, bool isLocked, string category, WeaponMerchant weaponMerchant) : base(citizens, isLocked, category)
        {
            WeaponMerchant = weaponMerchant;
        }

        public void DisplayList(object arg = null)
        {
            WeaponMerchant.DisplayList();
        }

        public override void Enter(object arg = null)
        {
            GameMenu.Game.Statement = GameStatement.InBuilding;
            GameMenu.Game.CurrentBuilding.SetCurrentBuilding(this);
        }
    }
}
