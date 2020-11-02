using ConsoleGame.items;
using ConsoleGame.UI.lists;
using ConsoleGame.utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGame.misc.inventory
{
    public class Inventory
    {
        public IList<Item> Items { get; set; }
        public int ItemsPerPage { get; set; } = 10;
        public SelectionList<ListItem<Item>> Listing { get; protected set; }
        public int MaxLength { get; set; } = -1;

        public Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(int slots)
        {
            Items = new List<Item>();
            MaxLength = slots;
        }

        [JsonConstructor]
        public Inventory(List<Item> items, int maxLength, int itemsPerPage)
        {
            Items = items;
            MaxLength = maxLength;
            ItemsPerPage = itemsPerPage;
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

        public Inventory Add(Item item)
        {
            if(Items.Count < MaxLength || MaxLength == -1)
            {
                Items.Add(item);
            }
            return this;
        }

        public void Display(object args = null)
        {

            if (Items.Count > 0)
            {
                Console.Clear();
                Paginate();
            }
            else
            {
                Utils.Endl();
                Utils.Cconsole.Red.WriteLine("There is no item.");
            }
        }

        private void Paginate()
        {
            IEnumerable<IGrouping<string, Item>> groupedItems = Items.GroupBy(i => i.Name);
            List<ListItem<Item>> listItems = new List<ListItem<Item>>();
            foreach(IGrouping<string, Item> grouping in groupedItems)
            {
                listItems.Add(new ListItem<Item>(grouping.ElementAt(0)));
            }

            listItems.GroupBy(i => i.Item.Name);

            Listing = new SelectionList<ListItem<Item>>(
                listItems,
                new ItemListing<ListItem<Item>>(DisplayAction),
                "You left the inventory",
                ItemsPerPage
            )
            {
                InitEventAction = new ItemListing<ListItem<Item>>(InitEvent),
                DeconstructEventAction = new ItemListing<ListItem<Item>>(DeconstructEvent),
                InitListItem = new InitListing<ListItem<Item>>(InitListItem),
                Header = Header,
                ErrorMarginTop = 2
            };
            Listing.Paginate();
        }

        private void Header()
        {
            StringBuilder sb = new StringBuilder();

            string[] columns = new string[] { "name", "sell value", "amount" };

            sb.Append(columns[0].PadRight(40));
            sb.Append(columns[1].PadRight(40 - columns[1].Length));
            sb.Append(columns[2]);
            sb.AppendLine();

            Utils.Cconsole.Hex("#ff4540").WriteLine(sb.ToString());
        }

        private void DisplayAction(ListItem<Item> item)
        {
            item.DisplayText();
        }

        private void InitListItem(ListItem<Item> item, int cursorPosition)
        {
            item.Init(cursorPosition);
            
            IEnumerable<IGrouping<string, Item>> groupedItems = Items.GroupBy(i => i.Name);
            StringBuilder sb = new StringBuilder();

            foreach(IGrouping<string, Item> grouping in groupedItems)
            {
                if(grouping.Key == item.Item.Name)
                {
                    sb.Append(item.Item.Name.PadRight(40));
                    sb.Append(item.Item.Coins.ToString().PadRight(30));
                    sb.Append(grouping.ToList().Count);
                    item.Text = sb.ToString();
                }
            }
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
