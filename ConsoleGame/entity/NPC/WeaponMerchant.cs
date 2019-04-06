﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame.entity.NPC
{
    public class WeaponMerchant : AbstractNPC
    {
        public Weapon[] Weapons { get; private set; }

        [JsonConstructor]
        public WeaponMerchant(string name, string category, Weapon[] weapons) : base(name, category)
        {
            Weapons = weapons;
        }

        public void DisplayList()
        {
            Utils.Endl();
            for (int i = 0; i < Weapons?.Length; ++i)
            {
                Weapon weapon = Weapons[i];

                Console.WriteLine("{0}:   {1}", i + 1, weapon.Name);
            }
            Utils.Endl();
        }
    }
}
