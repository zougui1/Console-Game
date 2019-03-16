using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc;

namespace ConsoleGame
{
    public static class Utils
    {
        static public CConsole Cconsole { get; } = new CConsole();
        public static void Endl(int i = 1)
        {
            for(int j = 0; j < i; ++j)
            {
                Console.Write(Environment.NewLine);
            }
        }

        #region Caller methods
        public static Object Caller(string className, string methodName, object[] parameters = null)
        {
            Type type = Type.GetType(className);
            MethodInfo methodInfo = type.GetMethod(methodName);
            Object obj = Activator.CreateInstance(type);
            methodInfo.Invoke(obj, parameters);

            return obj;
        }

        public static Object Caller(object obj, string methodName, object[] parameters = null)
        {
            Type type = obj.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName);
            if (methodInfo == null)
            {
                Endl();
                Cconsole.Color("DarkRed").WriteLine("This command doesn't exists \"{0}\"", methodName);
                Endl();
                MethodInfo help = type.GetMethod("Help");
                help.Invoke(obj, null);
            }
            else
            {
                methodInfo.Invoke(obj, parameters);
            }

            return obj;
        }
        #endregion

        public static PropertyInfo[] GetProperties(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            return properties;
        }

        public static bool InEnum(string needle, Type enumType)
        {
            foreach(string item in Enum.GetNames(enumType))
            {
                if(item == needle)
                {
                    return true;
                }
            }

            return false;
        }

        public static int TryParseConsoleCin(string errorMessage = "", string parameter = "", string color = "White")
        {
            string input = Console.ReadLine();
            int parsed;
            if (int.TryParse(input, out parsed))
            {
                string[] param = parameter.Split(':');
                if (param[0] == "range")
                {
                    string[] numbers = param[1].Split('-');
                    int min;
                    int max;
                    int.TryParse(numbers[0], out min);
                    int.TryParse(numbers[1], out max);

                    if (parsed >= min && parsed <= max)
                    {
                        return parsed;
                    }
                }
                else
                {
                    return parsed;
                }
            }

            Cconsole.Color(color).WriteLine(errorMessage);
            TryParseConsoleCin(errorMessage, parameter, color);
            return 0;
        }

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
