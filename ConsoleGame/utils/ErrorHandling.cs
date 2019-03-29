using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc;

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
                Console.SetCursorPosition(0, cursorPosition);
                FillLine();
                Console.SetCursorPosition(0, newCursorPosition);
            }, 1000);
        }
    }
}
