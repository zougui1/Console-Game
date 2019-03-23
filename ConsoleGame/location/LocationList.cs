using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.json;

namespace ConsoleGame.location
{
    public class LocationList
    {
        /// <summary>
        /// LocationsDict represent a dictionnary with for key a named tuple which contains the coords of a location, and for value the id of the location
        /// </summary>
        public static IDictionary<(int X, int Y), int> LocationsDict { get; set; }

        /// <summary>
        /// SetLocations is used to add all the locations in the property LocationsDict from the data in json
        /// </summary>
        public static void SetLocations()
        {
            LocationsDict = new Dictionary<(int X, int Y), int>();
            Json.GetLocations(LocationsDict);
        }
    }
}
