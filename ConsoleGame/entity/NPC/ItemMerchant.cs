﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.items;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame.entity.NPC
{
    public class ItemMerchant : AbstractNPC
    {
        public Item[] Items { get; private set; }

        [JsonConstructor]
        public ItemMerchant(string name, string category, Item[] items) : base(name, category)
        {
            Items = items;
        }

        public void DisplayList()
        {
            Utils.Endl();
            for (int i = 0; i < Items.Length; ++i)
            {
                Item item = Items[i];

                Console.WriteLine("{0}:   {1}", i + 1, item.Name);
            }
            Utils.Endl();
        }
    }
}
