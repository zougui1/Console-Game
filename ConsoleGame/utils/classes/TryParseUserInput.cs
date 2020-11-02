using System;

namespace ConsoleGame.utils.classes
{
    public class TryParseUserInput
    {
        public string Parameter { get; set; } = "";
        public string ErrorColor { get; set; } = "DarkRed";
        public int ErrorTopPosition { get; set; }
        public string ErrorMessage { get; set; }

        public TryParseUserInput(string errorMessage = "", int? errorTopPosition = null)
        {
            ErrorMessage = errorMessage;
            ErrorTopPosition = errorTopPosition ?? Console.CursorTop;
        }

        public int While()
        {
            bool validInput = false;
            int parsed = 0;

            while (!validInput)
            {
                string input = Console.ReadLine();

                if (int.TryParse(input, out parsed))
                {
                    string[] param = Parameter.Split('-');
                    if (param[0] == "range")
                    {
                        string[] numbers = param[1].Split('-');
                        int.TryParse(numbers[0], out int min);
                        int.TryParse(numbers[1], out int max);

                        if (parsed >= min && parsed <= max)
                        {
                            validInput = true;
                        }
                    }
                    else
                    {
                        validInput = true;
                    }
                }

                if (!validInput)
                {
                    Utils.ErrorHandling(ErrorMessage, ErrorTopPosition, ErrorColor);
                }
            }

            return parsed;
        }
    }
}
