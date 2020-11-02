using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void ErrorHandling(string message, int cursorPosition, string color = "DarkRed")
        {
            Console.SetCursorPosition(0, cursorPosition);
            Cconsole.Color(color).WriteLine(message);
            SetTimeout(() =>
            {
                int newCursorPosition = Console.CursorTop;
                newCursorPosition = ((newCursorPosition - cursorPosition) > 2) ? newCursorPosition : cursorPosition;

                Console.SetCursorPosition(0, cursorPosition);
                FillLine();
                Console.SetCursorPosition(0, newCursorPosition);
            }, 1000);
        }
    }
}
