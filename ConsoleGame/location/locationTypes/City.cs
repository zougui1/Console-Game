using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.building;
using ConsoleGame.misc.coords;

namespace ConsoleGame.location.locationTypes
{
    public class City : Location
    {
        public City(string name, Coords coords, Building[] buildings) : base(name, coords, buildings)
        {
            Type = "City";
        }
        
        public void Display()
        {
            Console.WriteLine(Name);
            Citizens[0].Discussion();
        }
    }
}
