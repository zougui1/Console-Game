using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.utils;

namespace ConsoleGame.UI
{
    public class Pagination
    {
        /// <summary>
        /// PageChanged is triggered when the page of the pagination changed
        /// </summary>
        public event Action PageChanged;
        /// <summary>
        /// PaginationExited is triggered when the user quit the pagination 
        /// </summary>
        public event Action PaginationExited;

        /// <summary>
        /// ListCount represent the number of elements we want to paginate over
        /// </summary>
        public int ListCount { get; private set; }
        /// <summary>
        /// ItemsPerPage represent the number of objects we want in a page
        /// </summary>
        public int ItemsPerPage { get; private set; }
        /// <summary>
        /// Page represent the current page of the pagination
        /// </summary>
        public int Page { get; private set; } = 1;
        /// <summary>
        /// ChangePage represent wether or no we want to change of page
        /// </summary>
        protected bool ChangePage { get; set; } = false;
        /// <summary>
        /// ExitPagination represent wether or no we want to quit the pagination
        /// </summary>
        protected bool ExitPagination { get; set; } = false;
        /// <summary>
        /// ExitMessage represent the message to display when the user quit the pagination
        /// </summary>
        public string ExitMessage { get; set; } = "You left the interface";
        /// <summary>
        /// Action represent the action to call each time a page has been changed
        /// </summary>
        protected PaginateAction Action { get; set; }
        /// <summary>
        /// HideCursor define wether or no we want to hide the cursor during the pagination
        /// </summary>
        public bool HideCursor { get; set; } = true;
        /// <summary>
        /// PageInfos represent the message to display at the right-bottom of the pagination about informations of the page
        /// if null a default message is displayed
        /// </summary>
        public string PageInfos { get; set; } = null;
        /// <summary>
        /// DefaultKeyPress is an action to call when a key that is not tested by the class has been triggered
        /// if null by default we display what has been written
        /// </summary>
        public DefaultKeyPress DefaultKeyPress { get; set; } = null;
        /// <summary>
        /// LastPage represent the last page possible of the pagination
        /// </summary>
        protected int LastPage { get; set; }

        public Pagination(int listCount, PaginateAction action = null, int itemsPerPage = 10)
        {
            ListCount = listCount;
            ItemsPerPage = itemsPerPage;
            Action = action;
        }

        public Pagination(int listCount, string exitMessage, int itemsPerPage = 10, PaginateAction action = null, bool hideCursor = true)
        {
            ListCount = listCount;
            ItemsPerPage = itemsPerPage;
            Action = action;
            ExitMessage = exitMessage;
            HideCursor = hideCursor;
        }

        /// <summary>
        /// GetLastPage is used to get the last possible page
        /// </summary>
        /// <returns>the number of total pages</returns>
        protected int GetLastPage()
        {
            int addPage = (ListCount % ItemsPerPage > 0) ? 1 : 0;
            int lastPage = ListCount / ItemsPerPage + addPage;
            return lastPage;
        }

        /// <summary>
        /// CallBeforeKeyTesting used to get overriden
        /// </summary>
        public virtual void CallBeforeKeyTesting()
        {

        }

        /// <summary>
        /// Paginate is used to paginate over the list
        /// </summary>
        public virtual void Paginate()
        {
            if (HideCursor) {
                Console.CursorVisible = false;
            }

            ExitPagination = false;
            LastPage = GetLastPage();

            PaginateExitWhile();

            PaginationExited?.Invoke();
        }

        /// <summary>
        /// PäginateExitWhile contains the while loop that is true until the user quit the pagination
        /// </summary>
        public void PaginateExitWhile()
        {
            while (!ExitPagination)
            {
                ChangePage = false;
                (int min, int max) = GetMinAndMaxIndex();
                CallAction(min, max);
                Footer(min, max);
                int entries = max - min;
                int errorPosition = entries + 3;

                CallBeforeKeyTesting();

                PaginateChangePageWhile(errorPosition);

                PageChanged?.Invoke();
            }
        }

        /// <summary>
        /// PaginayeChangePageWhile contains the while loop that is true until the user change of page
        /// </summary>
        /// <param name="errorPosition"></param>
        private void PaginateChangePageWhile(int errorPosition)
        {
            while (!ChangePage)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        PreviousPage(errorPosition);
                        break;
                    case ConsoleKey.RightArrow:
                        NextPage(LastPage, errorPosition);
                        break;
                    case ConsoleKey.Escape:
                    case ConsoleKey.Enter:
                        Exit();
                        break;
                    default:
                        if (DefaultKeyPress != null)
                        {
                            DefaultKeyPress(key);
                        }
                        else
                        {
                            string text = Console.ReadLine();
                            Utils.Cconsole.Color("Cyan").Write(key.KeyChar + text);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// GetMinAndMaxIndex is used to get the min and max index of the current page
        /// </summary>
        /// <returns>returns a value tuple with the min and max index of the current page</returns>
        protected (int min, int max) GetMinAndMaxIndex()
        {
            int startIndex = (Page - 1) * ItemsPerPage;
            int maxIndex = (Page * ItemsPerPage) >= ListCount ? ListCount : Page * ItemsPerPage;

            return (startIndex, maxIndex);
        }
        /// <summary>
        /// GetMinAndMaxIndex is used to get the min and max index of the given page
        /// </summary>
        /// <param name="page">the page which we want to get the min and max index</param>
        /// <returns>returns a value tuple with the min and max index of the given page</returns>
        protected (int min, int max) GetMinAndMaxIndex(int page)
        {
            int startIndex = (page - 1) * ItemsPerPage;
            int maxIndex = (page * ItemsPerPage) >= ListCount ? ListCount : page * ItemsPerPage;

            return (startIndex, maxIndex);
        }

        /// <summary>
        /// CallAction is used to clear the console and call the Action property
        /// </summary>
        /// <param name="min">the min index of the page</param>
        /// <param name="max">the max index of the page</param>
        private void CallAction(int min, int max)
        {
            Console.Clear();
            Action(min, max);
        }

        /// <summary>
        /// Footer is used to display the current page and some informations of the current page at the bottom of the pagination
        /// </summary>
        /// <param name="min">the min index of the current page</param>
        /// <param name="max">the max index of the page</param>
        public void Footer(int min, int max)
        {
            Utils.Endl();
            Utils.Cconsole.Color("Green").Write("page {0}", Page);

            if (PageInfos == null)
            {
                Utils.Cconsole.Right().Absolute().Offset(0).Color("Green").WriteLine($"{min}/{max} over {ListCount} items");
            }
            else
            {
                Utils.Cconsole.Right().Absolute().Offset(0).Color("Green").WriteLine(PageInfos);
            }

            Utils.Endl();
        }

        /// <summary>
        /// PreviousPage is used to go to the previous page if possible, otherwise display an error to the user
        /// </summary>
        /// <param name="errorPosition">the top position where we want to display the error</param>
        private void PreviousPage(int errorPosition)
        {
            if (Page == 1)
            {
                ErrorHandling("The page can't be less than 1", errorPosition);
            }
            else
            {
                --Page;
                ChangePage = true;
            }
        }

        /// <summary>
        /// NextPage is used to go to the next page if possible, otherwise display an error to the user
        /// </summary>
        /// <param name="errorPosition">the top position where we want to display the error</param>
        private void NextPage(int lastPage, int errorPosition)
        {
            if (lastPage == Page)
            {
                ErrorHandling($"The page can't be more than {lastPage}", errorPosition);
            }
            else
            {
                ++Page;
                ChangePage = true;
            }
        }

        /// <summary>
        /// Exit is used to display the ExitMessage and define both pagination loops condition to true (the condition in the loops are inversed)
        /// </summary>
        private void Exit()
        {
            Utils.Cconsole.Color("Blue").WriteLine(ExitMessage);
            ExitPagination = true;
            ChangePage = true;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// ErrorHandling is used to display an error to the user to a specific position and delete it after 1 second
        /// </summary>
        /// <param name="message">the error message to display</param>
        /// <param name="cursorPosition">the top position where we want to display the error</param>
        private void ErrorHandling(string message, int cursorPosition)
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
