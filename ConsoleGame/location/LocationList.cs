using ConsoleGame.json;
using System.Collections.Generic;

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
