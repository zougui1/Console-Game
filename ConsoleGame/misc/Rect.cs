using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.misc
{
    public struct Rect
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Rect(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
