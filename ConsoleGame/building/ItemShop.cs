using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.items;

namespace ConsoleGame.building
{
    public class ItemShop : Shop
    {
        public Item[] Items { get; set; }

        public ItemShop() : base()
        {

        }

        public void DisplayList()
        {
            for (int i = 0; i < Items.Length; ++i)
            {
                Item item = Items[i];

                Console.WriteLine("{0}:   {1}", i, item.Name);
            }
        }
    }
}
