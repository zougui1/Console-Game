using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void Endl(int i = 1)
        {
            for(int j = 0; j < i; ++j)
            {
                Console.Write(Environment.NewLine);
            }
        }
    }
}
