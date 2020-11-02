using System.Collections.Generic;

namespace ConsoleGame.UI.lists
{
    public class Listing<TList> : Pagination
    {
        /// <summary>
        /// List contains the objects we want to display
        /// </summary>
        public IList<TList> List { get; protected set; }
        /// <summary>
        /// Striped define wether or no we want to apply a striped style to the list
        /// </summary>
        public bool Striped { get; set; } = true;
        /// <summary>
        /// ColorEven is the color of the even elements of the list
        /// </summary>
        public string ColorEven { get; set; } = "Gray";
        /// <summary>
        /// ColorOdd is the color of the odd elements of the list
        /// </summary>
        public string ColorOdd { get; set; } = "DarkGray";

        protected Listing() : base() { }
        public Listing(IList<TList> list, ItemListing<TList> action, int itemsPerPage = 10)
            : base(list.Count, itemsPerPage: itemsPerPage)
        {
            List = list;
            Action = PaginationAction(action);
        }

        public Listing(IList<TList> list, ItemListing<TList> action, string exitMessage, int itemsPerPage = 10, bool hideCursor = true)
            : base(list.Count, exitMessage, itemsPerPage, hideCursor: hideCursor)
        {
            List = list;
            Action = PaginationAction(action);
        }

        public Listing(IList<TList> list, PaginateAction action, int itemsPerPage = 10)
            : base(list.Count, action, itemsPerPage)
        {
            List = list;
        }

        public Listing(IList<TList> list, PaginateAction action, string exitMessage, int itemsPerPage = 10, bool hideCursor = true)
            : base(list.Count, exitMessage, itemsPerPage, action, hideCursor)
        {
            List = list;
        }

        /// <summary>
        /// PaginationAction is used to "convert" an ItemListing action into a PaginateAction
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected PaginateAction PaginationAction(ItemListing<TList> action)
        {
            return new PaginateAction((int min, int max) =>
            {
                PaginateAction(min, max, action);
            });
        }

        /// <summary>
        /// PaginateAction is used as an action to iterates over the list starting from the given minimum index ending to the given maximum index
        /// </summary>
        /// <param name="min">starting index</param>
        /// <param name="max">ending index</param>
        /// <param name="action">action to trigger</param>
        private void PaginateAction(int min, int max, ItemListing<TList> action)
        {
            for (int i = min; i < max; ++i)
            {
                action(List[i]);
            }
        }
    }
}
