using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.misc.coords
{
    public class Coords
    {
        // positive = right; negative = left
        public int X { get; protected set; }
        // positive = bottom; negative = top
        public int Y { get; protected set; }

        public Coords(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }
    }
}
