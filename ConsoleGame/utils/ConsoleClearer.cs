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
        public static void ConsoleClearer()
        {
            Console.ReadKey();
            Console.Clear();
        }

        public static void ConsoleClearer(string message, string color = "Red")
        {
            Cconsole.Color(color).WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        public static void ConsoleClearer(int milliseconds)
        {
            SetTimeoutSync(() =>
            {
                Console.Clear();
            }, milliseconds);
        }
    }
}
