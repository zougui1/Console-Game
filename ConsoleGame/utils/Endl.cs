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
        /// Endl is used to skip one line or more depending of the argument
        /// </summary>
        /// <param name="i">defined the number of time to skip a line</param>
        public static void Endl(int i = 1)
        {
            for(int j = 0; j < i; ++j)
            {
                Console.Write(Environment.NewLine);
            }
        }
    }
}
