using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// Choices is used to display all the given choices in the console, let the user which choice they choose, and call the method depending on what they choosed
        /// </summary>
        /// <param name="choices">contains the choices available</param>
        /// <param name="actions">contains the methods to call once the user choosed</param>
        /// <param name="args">the arguments to pass to the method to be called</param>
        /// <param name="color">the color of the choices in the console</param>
        /// <param name="parameter">contains the list of spells to cast, if defined</param>
        public static void Choices(string[] choices, Action[] actions, object[][] args = null, string color = "Gray", List<Spell> parameter = null, int removeLines = 0)
        {
            for (int i = 0; i < choices.Length; ++i)
            {
                if (choices[i] != null)
                {
                    Cconsole.Color(color).WriteLine("{0}. {1}", i + 1, choices[i]);
                }
            }

            bool rightAction = false;

            while (!rightAction)
            {
                int action = TryParseConsoleCin("You must enter a valid number", color: "DarkRed");
                --action;
                DeletePreviousLine(removeLines);

                try
                {
                    if (choices[action] == null)
                    {
                        throw new Exception();
                    }

                    object[] newArgs = new object[2];

                    if (args != null)
                    {

                        if (parameter != null)
                        {
                            newArgs[0] = args[action];
                            newArgs[1] = parameter[action];
                            actions[action](newArgs);
                        }
                        else
                        {
                            actions[action](args[action]);
                        }
                    }
                    else
                    {
                        if (parameter != null)
                        {
                            newArgs[0] = parameter[action];
                        }

                        actions[action](newArgs);
                    }
                    rightAction = true;
                }
                catch(IndexOutOfRangeException e)
                {
                    ErrorHandling("You must enter a number that match an action.", Console.CursorTop);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
