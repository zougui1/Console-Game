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
        public static void FillLine()
        {
            for (int i = 0; i < Console.WindowWidth; i++)
            {
                Console.Write(' ');
            }
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
