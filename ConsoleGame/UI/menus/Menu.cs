using ConsoleGame.utils;
using ConsoleGame.utils.classes;
using System;
using System.Collections.Generic;

using ConsoleGame.UI.lists;

namespace ConsoleGame.UI.menus
{
    public class Menu<TAction, TArgs> : SelectionList<ListItem<string>>
    {
        public string Label { get; protected set; }
        public List<string> Choices { get; protected set; }
        public List<TAction<TArgs>> Actions { get; protected set; }
        public List<TArgs> Args { get; protected set; }
        public int RemoveLines { get; set; }
        public string ChoicesColor { get; set; } = "Gray";
        public string LabelColor { get; set; } = "DarkGray";
        public string ErrorColor { get; set; } = "DarkRed";
        public int ChoosedAction { get; protected set; }
        public int ActionIndex { get; protected set; }
        public string ChoosedActionMessage { get; set; } = "(action: 0)";
        public string ChoosedActionColor { get; set; } = "Gray";
        public bool RightAction { get; protected set; } = false;
        public int FirstLinePositionTop { get; protected set; }
        public int ErrorPositionTop { get; protected set; }
        public bool UseChangedPageHandler { get; set; } = true;
        public bool SinglePage { get; set; } = false;
        // TODO, make an enum for the type
        public string Kind { get; set; }
        public TryParseUserInput Parser { get; set; } = new TryParseUserInput("You must enter a valid number");
        // TODO, parameter should be of a generic type
        public bool parameter { get; set; } = false;
        public bool WithFooter { get; set; } = true;

        public Menu(string label, int removeLines = 1)
            : base()
        {
            Label = label;
            Choices = new List<string>();
            Actions = new List<TAction<TArgs>>();
            Args = new List<TArgs>();
            RemoveLines = removeLines;
        }

        ///
        protected override void Footer(int min, int max)
        {
            if (WithFooter)
            {
                Utils.Endl();
                Utils.Cconsole.Green.Write("page {0}/{1}", Page, GetLastPage());
                Utils.Endl();
            }
            else
            {
                Utils.Endl();
                Console.Write("");
                Utils.Endl();
            }
        }

        public void InitSelection()
        {
            if(Kind == "UI")
            {
                WithFooter = false;
            }

            if (SinglePage)
            {
                UseChangedPageHandler = false;
                HandleErrors = false;
            }

            ColorOdd = ColorEven;
            List = new List<ListItem<string>>();
            Choices.ForEach(choice => List.Add(new ListItem<string>(choice)));
            Action = PaginationAction(new ItemListing<ListItem<string>>(DisplayAction));
            ListCount = Choices.Count;
            ItemsPerPage = 8;
            ExitMessage = null;

            InitEventAction = new ItemListing<ListItem<string>>(InitEvent);
            DeconstructEventAction = new ItemListing<ListItem<string>>(DeconstructEvent);
            InitListItem = new InitListing<ListItem<string>>(InitListItemAction);
            DefaultKeyPressSub = DefaultKeyPressSubAction;

            DefaultKeyPress = new DefaultKeyPress(DefaultKeyPressAction);
            if (UseChangedPageHandler)
            {
                PageChanged += new Action(ChangePageHandler);
            }
            PaginationExited += new Action(() => EventDestructor(0, List.Count - 1));

            Utils.Cconsole.Color(LabelColor).WriteLine(Label);
            CursorTop = Console.CursorTop;

            Paginate();
        }

        private void DefaultKeyPressSubAction(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    ClearList(List.Count + 4, false);
                    ChoosedAction = CurrentCursorTop;
                    ActionIndex = CurrentCursorTop;
                    TryAction();
                    break;
            }
        }

        private void DisplayAction(ListItem<string> item)
        {
            item.DisplayText();
        }

        private void InitListItemAction(ListItem<string> item, int cursorPosition)
        {
            item.OddColor = ColorOdd;
            item.Init(cursorPosition);
        }

        private void InitEvent(ListItem<string> item)
        {
            LineChanged += item.HandleFocus;
        }

        private void DeconstructEvent(ListItem<string> item)
        {
            LineChanged -= item.HandleFocus;
        }

        protected override void ClearList(int entries, bool initializing)
        {
            if (!initializing)
            {
                Utils.DeletePreviousLine(entries);
            }
        }

        /// <summary>
        /// Exit is used to display the ExitMessage and define both pagination loops condition to true (the condition in the loops are inversed)
        /// </summary>
        protected override void Exit()
        {
            ExitPagination = true;
            ChangePage = true;
            Console.CursorVisible = true;
        }
        ///

        public Menu<TAction, TArgs> AddChoice(string choice, TAction<TArgs> action)
        {
            Choices.Add(choice);
            Actions.Add(action);
            return this;
        }

        public Menu<TAction, TArgs> AddChoice(string choice, TAction<TArgs> action, TArgs args)
        {
            Choices.Add(choice);
            Actions.Add(action);
            Args.Add(args);
            return this;
        }

        public Menu<TAction, TArgs> AddChoices(List<string> choices, List<TAction<TArgs>> actions)
        {
            Choices.AddRange(choices);
            Actions.AddRange(actions);
            return this;
        }

        public Menu<TAction, TArgs> AddChoices(List<string> choices, List<TAction<TArgs>> actions, List<TArgs> args)
        {

            Choices.AddRange(choices);
            Actions.AddRange(actions);
            Args.AddRange(args);
            return this;
        }

        public Menu<TAction, TArgs> AddChoices(List<string> choices)
        {
            Choices.AddRange(choices);
            return this;
        }

        public Menu<TAction, TArgs> AddActions(List<TAction<TArgs>> actions)
        {
            Actions.AddRange(actions);
            return this;
        }

        public Menu<TAction, TArgs> AddArgs(List<TArgs> args)
        {
            Args.AddRange(args);
            return this;
        }

        public virtual void Choose()
        {
            FirstLinePositionTop = Console.CursorTop;
            ErrorPositionTop = Console.CursorTop + Choices.Count + 1;

            Parser.ErrorTopPosition = ErrorPositionTop;
            Parser.ErrorColor = ErrorColor;

            DisplayChoices();
            GetAction();
        }

        protected void DisplayChoices()
        {
            if ((Label?.Length ?? 0) > 0)
            {
                ChoosedActionMessage = $" {ChoosedActionMessage}";
            }

            Utils.Cconsole.Color(LabelColor).Write(Label).Color(ChoosedActionColor).WriteLine(ChoosedActionMessage);
            for (int i = 0; i < Choices.Count; ++i)
            {
                if (Choices[i] != null)
                {
                    string choice = $"{i + 1}. {Choices[i]}";
                    Utils.Cconsole.Color(ChoicesColor).Write(choice);
                    Utils.Endl();
                }
            }
        }

        protected void GetAction()
        {
            while (!RightAction)
            {
                ChoosedAction = Parser.While();
                //ChoosedAction = Utils.TryParseConsoleCin("You must enter a valid number", color: ErrorColor, cursorTop: ErrorPositionTop);
                Utils.DeletePreviousLine(RemoveLines);
                ActionIndex = ChoosedAction - 1;
                TryAction();
            }
        }

        protected void TryAction()
        {
            try
            {
                ThrowIfNull(Choices[ActionIndex], $"the choice at index \"{ActionIndex}\" must not be null");

                if(Kind != "UI")
                {
                    Utils.Cconsole.Absolute().Top(FirstLinePositionTop).Offset((Label?.Length ?? 0) + ChoosedActionMessage.Length - 2).Color(ChoosedActionColor).WriteLine("{0})", ChoosedAction);
                }
                else
                {
                    Exit();
                    TriggerPaginationExited();
                }

                if (Args.Count <= ActionIndex)
                {
                    TArgs[] temp = new TArgs[1];
                    Actions[ActionIndex](temp[0]);
                }
                else
                {
                    Actions[ActionIndex](Args[ActionIndex]);
                }

                //RightAction = true;
            }
            catch (IndexOutOfRangeException e)
            {
                ErrorHandling("You must enter a number that match an action.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //throw e;
            }
        }

        protected void ThrowIfNull(object element, string message)
        {
            if (element == null)
            {
                throw new Exception(message);
            }
        }

        protected void ErrorHandling(string message)
        {
            Console.SetCursorPosition(0, ErrorPositionTop);
            Utils.Cconsole.Color(ErrorColor).WriteLine(message);
            Utils.SetTimeout(() =>
            {
                Console.SetCursorPosition(0, ErrorPositionTop);
                Utils.FillLine();
                Utils.FillLine();
                if (!RightAction)
                {
                    Console.SetCursorPosition(0, ErrorPositionTop);
                }
            }, 1000);
        }
    }
}
