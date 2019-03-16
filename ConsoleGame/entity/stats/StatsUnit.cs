using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame
{
    public class StatsUnit
    {
        public double Unit { get; private set; }
        public int Minus2 { get; private set; }
        public int Minus1 { get; private set; }
        public int None { get; private set; }
        public int Plus1 { get; private set; }
        public int Plus2 { get; private set; }

        public StatsUnit(int minus2, int minus1, int none, int plus1, int plus2 = 2, double unit = 1)
        {
            Minus2 = minus2;
            Minus1 = minus1;
            None = none;
            Plus1 = plus1;
            Plus2 = plus2;
            Unit = unit;
        }
    }
}
