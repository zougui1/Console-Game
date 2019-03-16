﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.items
{
    public class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public Item(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
