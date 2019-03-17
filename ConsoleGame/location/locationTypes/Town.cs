using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.building;
using ConsoleGame.misc.coords;

namespace ConsoleGame.location.locationTypes
{
    public class Town : Location
    {
        Town(string name, Coords coords, Building[] buildings) : base(name, coords, buildings)
        {
            Type = "Town";
        }
    }
}
