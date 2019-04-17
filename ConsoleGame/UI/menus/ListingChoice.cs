using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.utils;

namespace ConsoleGame.UI.menus
{
    public class ListingChoices<TAction, TArgs> : Pagination
    {
        // use of pagination and listing with the menu (e.g. a listing of the spells of the character)
        public Menu<TAction, TArgs> Menu { get; private set; }

        public ListingChoices(Menu<TAction, TArgs> menu, int itemsPerPage = 8)
            : base(menu.Choices.Count, itemsPerPage: itemsPerPage)
        {
            Menu = menu;
            Action = PaginateAction;
        }

        public ListingChoices(Menu<TAction, TArgs> menu, string exitMessage, int itemsPerPage = 8, bool hideCursor = false)
            : base(menu.Choices.Count, exitMessage, itemsPerPage, hideCursor: hideCursor)
        {
            Menu = menu;
            Action = PaginateAction;
        }

        public void Choose()
        {
            base.Paginate();
        }

        private void PaginateAction(int min, int max)
        {
            // doesn't work, to solve it just use the DefaultKeyPressAction property of Pagination
            // to do so, just transform Utils.TryParseConsoleCin into an object
            
            int count = max - min;

            int argsCount = count;
            int argsIndex = min;
            if (Menu.Args.Count < count) argsCount = Menu.Args.Count;
            if (Menu.Args.Count < min) argsIndex = Menu.Args.Count;

            Menu<TAction, TArgs> partialMenu = new Menu<TAction, TArgs>(Menu.Question)
                .AddChoices(Menu.Choices.GetRange(min, count))
                .AddActions(Menu.Actions.GetRange(min, count))
                .AddArgs(Menu.Args.GetRange(argsIndex, argsCount));
            partialMenu.Choose();
        }
    }
}
