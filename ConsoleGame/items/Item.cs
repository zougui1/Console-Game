using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.utils;

namespace ConsoleGame.items
{
    public class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public int Coins { get; protected set; }

        public Item(string name, string description, int coins = 0)
        {
            Name = name;
            Description = description;
            Coins = coins;
        }

        public virtual void Display(string color = "White")
        {
            Utils.Cconsole.Color(color).Write(Name.PadRight(36));
            Utils.Cconsole.Color(color).WriteLine("{0} coins", Coins);
        }
    }
}
