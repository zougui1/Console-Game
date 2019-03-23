using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleGame.building;
using ConsoleGame.entity.NPC;
using ConsoleGame.json;
using ConsoleGame.misc.coords;
using ConsoleGame.utils;

namespace ConsoleGame.location
{
    public class Location
    {
        public string Name { get; protected set; }
        /// <summary>
        /// Coords represent the position of the location in the map
        /// </summary>
        public Coords Coords { get; private set; }
        /// <summary>
        /// Buildings represent an array of the buildings in the location
        /// </summary>
        public Building[] Buildings { get; protected set; }
        /// <summary>
        /// Citizens represent an array of the citizens in the location
        /// </summary>
        public Citizen[] Citizens { get; protected set; }
        /// <summary>
        /// Type represent the type of the location (town, city, kingdom)
        /// </summary>
        public string Category { get; protected set; }
        public Church Church { get; protected set; }
        public ArmorShop ArmorShop { get; set; }
        public WeaponShop WeaponShop { get; set; }
        public ItemShop ItemShop { get; set; }
        /// <summary>
        /// Castle represent the castle in the kingdom (if it is)
        /// </summary>
        public dynamic Castle { get; protected set; }

        public Location(string name, Coords coords, Building[] buildings)
        {
            Name = name;
            Coords = coords;
            Buildings = buildings;
            Church = new Church();
        }

        public void InitAllBuildings()
        {
            for(int i = 0; i < Buildings.Length; ++i)
            {
               // Buildings[i].Init();
            }
        }

        public void Display()
        {
            Citizens[0].Discussion();
        }
    }
}
