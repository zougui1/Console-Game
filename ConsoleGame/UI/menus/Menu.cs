using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.utils;

namespace ConsoleGame.UI.menus
{
    public class Menu<TAction, TArgs>
    {
        public string Question { get; private set; }
        public List<string> Choices { get; private set; }
        public List<TAction<TArgs>> Actions { get; private set; }
        public List<TArgs> Args { get; private set; }
        public int RemoveLines { get; set; }
        public string ChoicesColor { get; set; } = "Gray";
        public string QuestionColor { get; set; } = "DarkGray";
        public string ErrorColor { get; set; } = "DarkRed";
        public int ChoosedAction { get; private set; }
        public int ActionIndex { get; private set; }
        public string ChoosedActionMessage { get; set; } = "(action: 0)";
        public string ChoosedActionColor { get; set; } = "Gray";
        public bool RightAction { get; private set; } = false;
        public int FirstLinePositionTop { get; private set; }
        public int ErrorPositionTop { get; private set; }
        // TODO, parameter should be of a generic type
        public bool parameter { get; set; } = false;

        public Menu(string question, int removeLines = 1)
        {
            Question = question;
            Choices = new List<string>();
            Actions = new List<TAction<TArgs>>();
            Args = new List<TArgs>();
            RemoveLines = removeLines;
        }

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

        public void Choose()
        {
            FirstLinePositionTop = Console.CursorTop;
            ErrorPositionTop = Console.CursorTop + Choices.Count;
            DisplayChoices();
            GetAction();
        }

        protected void DisplayChoices()
        {
            if((Question?.Length ?? 0) > 0)
            {
                ChoosedActionMessage = $" {ChoosedActionMessage}";
            }

            Utils.Cconsole.Color(QuestionColor).Write(Question).Color(ChoosedActionColor).WriteLine(ChoosedActionMessage);
            for(int i = 0; i < Choices.Count; ++i)
            {
                if(Choices[i] != null)
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
                ChoosedAction = Utils.TryParseConsoleCin("You must enter a valid number", color: ErrorColor, cursorTop: ErrorPositionTop);
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
                
                Utils.Cconsole.Absolute().Top(FirstLinePositionTop).Offset((Question?.Length ?? 0) + ChoosedActionMessage.Length - 2).Color(ChoosedActionColor).WriteLine("{0})", ChoosedAction);

                if (Args.Count <= ActionIndex)
                {
                    TArgs[] temp = new TArgs[1];
                    Actions[ActionIndex](temp[0]);
                }
                else
                {
                    Actions[ActionIndex](Args[ActionIndex]);
                }
                
                RightAction = true;
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
            if(element == null)
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
