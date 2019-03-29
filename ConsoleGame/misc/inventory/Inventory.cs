using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.items;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.json;
using ConsoleGame.utils;

namespace ConsoleGame.misc.inventory
{
    public class Inventory
    {
        public IList<Item> Items { get; set; }
        public int ItemsPerPage { get; protected set; }
        public int Page { get; protected set; }

        public Inventory()
        {
            Items = new List<Item>();
            ItemsPerPage = 10;
            Page = 1;
        }

        public Inventory(int slots)
        {
            Items = new Item[slots];
            ItemsPerPage = 10;
            Page = 1;
        }
        // for tests only
        /*public Inventory AddItem(int i = 1)
        {
            for (int j = 0; j < i; ++j)
            {
                Items.Add(new Item($"item {j}", "description"));
            }
            return this;
        }*/
        // for tests only
        /*public Inventory ReplaceItem(int i = 1)
        {
            for (int j = 0; j < i; ++j)
            {
                Items[j] = new Item($"item {j}", "description");
            }
            return this;
        }*/

        public void Display(object[] args = null)
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

        public void Paginate()
        {
            Console.CursorVisible = false;
            bool exitInventory = false;
            int addPage = (Items.Count % ItemsPerPage > 0) ? 1 : 0;
            int lastPage = Items.Count / ItemsPerPage + addPage;

            while (!exitInventory)
            {
                bool changePage = false;
                int entries = DisplayByPage();
                int errorPosition = entries + 3;

                while(!changePage)
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.RightArrow:
                            if (lastPage == Page)
                            {
                                ErrorHandling($"The page can't be more than {lastPage}", errorPosition);
                            }
                            else
                            {
                                ++Page;
                                changePage = true;
                            }
                            break;
                        case ConsoleKey.LeftArrow:
                            if (Page == 1)
                            {
                                ErrorHandling("The page can't be less than 1", errorPosition);
                            }
                            else
                            {
                                --Page;
                                changePage = true;
                            }
                            break;
                        case ConsoleKey.Escape:
                        case ConsoleKey.Enter:
                            exitInventory = true;
                            Utils.Cconsole.Color("Blue").WriteLine("You left the inventory");
                            Console.CursorVisible = true;
                            changePage = true;
                            break;
                        default:
                            string t = Console.ReadLine();
                            Utils.Cconsole.Color("Cyan").WriteLine(key.KeyChar + t);
                            break;
                    }
                }
            }
        }

        public int DisplayByPage()
        {
            int entries = 0;

            Console.Clear();
            int startIndex = (Page - 1) * ItemsPerPage;
            int maxIndex = (Page * ItemsPerPage) >= Items.Count ? Items.Count : Page * ItemsPerPage;

            for (int i = startIndex; i < maxIndex; ++i)
            {
                ++entries;
                string color;
                if(i % 2 == 0)
                {
                    color = "Gray";
                }
                else
                {
                    color = "DarkGray";
                }
                Items[i].Display(color);
            }

            Utils.Endl();
            Utils.Cconsole.Color("Green").Write("page {0}", Page);
            Utils.Cconsole.Color("Green").WriteLine("{0}/{1} over {2} items".PadLeft(110), startIndex, maxIndex, Items.Count);
            Utils.Endl();
            return entries;
        }

        public void Divide()
        {

        }

        public void ErrorHandling(string message, int cursorPosition)
        {
            Console.SetCursorPosition(0, cursorPosition);
            Utils.Cconsole.Color("Red").WriteLine(message);
            Utils.SetTimeout(() =>
            {
                Console.SetCursorPosition(0, cursorPosition);
                Utils.FillLine();
            }, 1000);
        }
    }
}
