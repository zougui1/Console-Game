using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.items;

namespace ConsoleGame.entity.NPC
{
    public class ItemMerchant : AbstractNPC
    {
        public Item[] Items { get; set; }
        public int[] ItemsId { get; set; }

        public ItemMerchant() : base()
        { }

        public void GetItemsById()
        {
            Items = new Item[ItemsId.Length];
            for(int i = 0; i < ItemsId.Length; ++i)
            {
                Items[i] = Json.GetItem(ItemsId[i]);
            }
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
