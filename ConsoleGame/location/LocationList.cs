using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleGame.location.locationTypes;

namespace ConsoleGame.location
{
    public class LocationList
    {
        public static IDictionary<(int X, int Y), int> LocationsDict { get; set; }

        public static void SetLocations()
        {
            LocationsDict = new Dictionary<(int X, int Y), int>();
            Json.GetLocations(LocationsDict);
        }
    }
}
