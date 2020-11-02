using ConsoleGame.items;
using System;

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
