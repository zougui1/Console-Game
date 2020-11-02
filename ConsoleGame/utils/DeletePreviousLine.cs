using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void DeletePreviousLine(int iterations = 1)
        {
            for (int i = 1; i <= iterations; ++i)
            {
                --Console.CursorTop;
                /*if(i > 1 && Console.CursorTop > 0)
                {
                    --Console.CursorTop;
                }*/
                FillLine();
                --Console.CursorTop;
            }
        }
    }
}
