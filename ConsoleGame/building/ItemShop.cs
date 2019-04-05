﻿using System;
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
        public ItemMerchant Merchant { get; private set; }

        [JsonConstructor]
        public ItemShop(List<AbstractNPC> npcs, bool isLocked, string category, ItemMerchant merchant) : base(npcs, isLocked, category)
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
