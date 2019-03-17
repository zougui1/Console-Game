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
        public static void Choices(string[] choices, Action[] actions, object[][] args = null, string color = "Gray", List<Spell> parameter = null)
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
                int action = TryParseConsoleCin("Please enter a valid number", color: "DarkRed");
                --action;

                try
                {
                    if (choices[action] == null)
                    {
                        throw new Exception();
                    }

                    object[] newArgs = new object[2];

                    if (args != null)
                    {

                        if(parameter != null)
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
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Cconsole.Color("DarkRed").WriteLine("Please enter a number that match an action.");
                }
            }
        }
    }
}
