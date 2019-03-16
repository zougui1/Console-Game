using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.misc.coords;

namespace ConsoleGame.location.locationTypes
{
    public class Kingdom : Location
    {
        public dynamic Castle { get; protected set; }

        public Kingdom(string name, Coords coords, dynamic[] buildings) : base(name, coords, buildings)
        {
            Type = "Kingdom";
        }
    }
}
