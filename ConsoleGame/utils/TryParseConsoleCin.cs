using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
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
    }
}
