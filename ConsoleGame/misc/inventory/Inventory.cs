using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using ConsoleGame.items;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.UI;
using ConsoleGame.UI.lists;
using ConsoleGame.utils;

namespace ConsoleGame.misc.inventory
{
    public class Inventory
    {
        public IList<Item> Items { get; set; }
        public int ItemsPerPage { get; protected set; } = 10;
        public SelectionList<ListItem<Item>> Listing { get; protected set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(int slots)
        {
            Items = new Item[slots];
        }

        [JsonConstructor]
        public Inventory(Item[] items)
        {
            Items = items;
        }

        // for tests only
        public Inventory AddItem(int i = 1)
        {
            for (int j = 0; j < i; ++j)
            {
                Items.Add(new Item($"item {j}", "description"));
            }
            return this;
        }
        // for tests only
        public Inventory ReplaceItem(int i = 1)
        {
            for (int j = 0; j < i; ++j)
            {
                Items[j] = new Item($"item {j}", "description");
            }
            return this;
        }

        public void Display(object args = null)
        {
            if(Items.Count > 0)
            {
                Paginate();
            }
            else
            {
                Utils.Endl();
                Utils.Cconsole.Color("Red").WriteLine("There is no item.");
            }
        }

        private void Paginate()
        {
            List<ListItem<Item>> listItems = new List<ListItem<Item>>();
            Items.ToList().ForEach(item => listItems.Add(new ListItem<Item>(item)));

            Listing = new SelectionList<ListItem<Item>>(
                listItems,
                new ItemListing<ListItem<Item>>(DisplayAction),
                "You left the inventory",
                ItemsPerPage
            );
            Listing.InitEventAction = new ItemListing<ListItem<Item>>(InitEvent);
            Listing.DeconstructEventAction = new ItemListing<ListItem<Item>>(DeconstructEvent);
            Listing.InitListItem = new InitListing<ListItem<Item>>(InitListItem);
            Listing.Paginate();
        }

        private void DisplayAction(ListItem<Item> item)
        {
            item.DisplayText();
        }

        private void InitListItem(ListItem<Item> item, int cursorPosition, string color)
        {
            item.Init(cursorPosition, color);
        }

        private void InitEvent(ListItem<Item> item)
        {
            Listing.LineChanged += item.HandleFocus;
        }

        private void DeconstructEvent(ListItem<Item> item)
        {
            Listing.LineChanged -= item.HandleFocus;
        }
    }
}
