﻿using System;
using System.Collections.Generic;

using ConsoleGame.items;
using ConsoleGame.utils;

namespace ConsoleGame.UI.lists
{
    public class SelectionList<TList> : Listing<TList>
    {
        /// <summary>
        /// LineChanged is triggered when the CurrentCursorTop change (essentially when up and down arrow keys are pressed)
        /// </summary>
        public event LineChangedHandler LineChanged;
        /// <summary>
        /// InitEventAction represent the action to link the List's objects with the event
        /// </summary>
        public ItemListing<TList> InitEventAction { get; set; }
        /// <summary>
        /// DeconstructEventAction represent the action to unlink the List's object from the event
        /// </summary>
        public ItemListing<TList> DeconstructEventAction { get; set; }
        /// <summary>
        /// InitListItem represent the action to init the List's objects
        /// </summary>
        public InitListing<TList> InitListItem { get; set; }
        /// <summary>
        /// CurrentCursorTop represent the current top position of the cursor
        /// </summary>
        public int CurrentCursorTop { get; protected set; } = 0;
        public int CurrentPosition { get; protected set; } = 0;
        public int InitialConsoleCursorTop { get; protected set; } = Console.CursorTop;
        public DefaultKeyPress DefaultKeyPressSub { get; set; }

        protected SelectionList() : base() { }
        public SelectionList(IList<TList> list, ItemListing<TList> action, int itemsPerPage = 10)
            : base(list, action, itemsPerPage)
        {
            PaginationInit();
        }

        public SelectionList(IList<TList> list, ItemListing<TList> action, string exitMessage, int itemsPerPage = 10, bool hideCursor = true)
            : base(list, action, exitMessage, itemsPerPage, hideCursor)
        {
            PaginationInit();
        }

        public SelectionList(IList<TList> list, PaginateAction action, int itemsPerPage = 10)
            : base(list, action, itemsPerPage)
        {
            PaginationInit();
        }

        public SelectionList(IList<TList> list, PaginateAction action, string exitMessage, int itemsPerPage = 10, bool hideCursor = true)
            : base(list, action, exitMessage, itemsPerPage, hideCursor)
        {
            PaginationInit();
        }

        /// <summary>
        /// PaginationInit is used to initialize the pagination
        /// </summary>
        protected void PaginationInit()
        {
            DefaultKeyPress = new DefaultKeyPress(DefaultKeyPressAction);
            PageChanged += new Action(ChangePageHandler);
            PaginationExited += new Action(() => EventDestructor(0, List.Count - 1));
        }

        /// <summary>
        /// DefaultKeyPressAction is used to get the pressed key when a key has been pressed and is not triggered by the pagination
        /// </summary>
        /// <param name="key">the pressed key</param>
        protected void DefaultKeyPressAction(ConsoleKeyInfo key)
        {
            int min, max;

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    (min, max) = GetMinAndMaxIndex();
                    ClearList(max - min, false);
                    Console.CursorTop = CursorTop;
                    Header?.Invoke();
                    LineChanged((CurrentCursorTop - 1) >= 0 ? --CurrentCursorTop : 0);
                    Footer(min, max);
                    break;
                case ConsoleKey.DownArrow:
                    (min, max) = GetMinAndMaxIndex();
                    ClearList(max - min, false);
                    Console.CursorTop = CursorTop;
                    Header?.Invoke();
                    LineChanged(((CurrentCursorTop + 1) < max) ? ++CurrentCursorTop : (max - 1));
                    Footer(min, max);
                    break;
                default:
                    if (DefaultKeyPressSub != null)
                    {
                        DefaultKeyPressSub(key);
                    }
                    else
                    {
                        string text = Console.ReadLine();
                        Utils.Cconsole.Cyan.Write(key.KeyChar + text);
                    }
                    break;
            }
        }

        /// <summary>
        /// ChangePageHandler is used to unlink all the List's object of the previous and next page from the event and link the List's objects of the current page with the event
        /// </summary>
        public void ChangePageHandler()
        {
            CurrentPosition = 0;
            if (Page > 1)
            {
                EventDestructor(Page - 1);
            }

            (int min, int max) = GetMinAndMaxIndex();

            for (int i = min; i < max; ++i)
            {
                if (i % 2 == 0)
                {
                    InitListItem(List[i], CurrentPosition++);
                }
                else
                {
                    InitListItem(List[i], CurrentPosition++);
                }

                InitEventAction(List[i]);
            }

            if (Page < LastPage)
            {
                EventDestructor(Page + 1);
            }

            CurrentCursorTop = 0;
            LineChanged(CurrentCursorTop);
        }

        /// <summary>
        /// unlink the List's objects of the given page from the event
        /// </summary>
        /// <param name="page">the page where the objects are</param>
        protected void EventDestructor(int page)
        {
            (int min, int max) = GetMinAndMaxIndex(page);

            for (int i = min; i < max; ++i)
            {
                DeconstructEventAction(List[i]);
            }
        }

        /// <summary>
        /// unlink the List's objects of the given minimum and maximum from the event
        /// </summary>
        /// <param name="min">index minimum</param>
        /// <param name="max">index maximum</param>
        protected void EventDestructor(int min, int max)
        {
            for (int i = min; i < max; ++i)
            {
                DeconstructEventAction(List[i]);
            }
        }

        /// <summary>
        /// Paginate is used to trigger the ChangePageHandler once and initialiaze the pagination
        /// </summary>
        public override void Paginate()
        {
            ChangePageHandler();
            base.Paginate();
        }

        /// <summary>
        /// CallBeforeKeyTesting is used to refresh the list before we record what the user enter and test the pressed key
        /// (if we don't when we change of page the first element of the list is not selected)
        /// </summary>
        public override void CallBeforeKeyTesting()
        {
            (int min, int max) = GetMinAndMaxIndex();
            ClearList(max - min, true);
            Header?.Invoke();
            LineChanged(CurrentCursorTop);
            Footer(min, max);
        }
    }
}
