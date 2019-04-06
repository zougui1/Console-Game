using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.items;

namespace ConsoleGame.entity.NPC
{
    public class Merchant
    {
        public virtual void DisplayItemList(Item[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];

                Console.WriteLine("{0}:   {1}", i + 1, item.Name);
            }
        }
    }
}
