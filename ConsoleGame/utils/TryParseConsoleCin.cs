using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// TryParseConsoleCin is used to get an entry from the user which is supposed to be an int and try to parse it
        /// the method call itself until the try parse succeed
        /// if the try parse succeed we return the parsed int
        /// a parameter can be given (a range between 2 numbers)
        /// </summary>
        /// <param name="errorMessage">the message to display in the console if the parse fail</param>
        /// <param name="parameter">parameter to add in for the return</param>
        /// <param name="color">the color of the error message to display in the console</param>
        /// <returns>return an int that the user entered</returns>
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
            return TryParseConsoleCin(errorMessage, parameter, color);
        }
    }
}
