using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void FillLine()
        {
            Console.CursorLeft = 0;
            for (int i = 0; i <= Console.WindowWidth; i++)
            {
                Console.Write(' ');
            }
            Console.CursorLeft = 0;
        }

        public static void FillLine(char character, string color = "White")
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Cconsole.Color(color).Write(character);
            }
        }
    }
}
