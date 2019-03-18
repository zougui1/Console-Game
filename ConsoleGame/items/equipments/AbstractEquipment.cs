﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.entity.stats;

namespace ConsoleGame.items.stuff
{
    public abstract class AbstractEquipment : Item
    {
        public Stats Stats { get; set; }
        public string Type { get; set; }
        public string TypeDescription { get; set; }

        public AbstractEquipment(string name, string description) : base(name, description)
        {
            Stats = new Stats();
        }
    }
}
