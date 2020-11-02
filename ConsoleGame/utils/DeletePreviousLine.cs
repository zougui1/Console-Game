using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void DeletePreviousLine(int iterations = 1)
        {
            for (int i = 0; i < iterations; ++i)
            {
                Console.CursorTop--;
                FillLine();
                Console.CursorTop--;
            }
        }
    }
}
