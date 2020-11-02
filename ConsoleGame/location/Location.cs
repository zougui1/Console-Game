using ConsoleGame.building;
using ConsoleGame.entity.NPC;
using ConsoleGame.misc.coords;
using Newtonsoft.Json;

namespace ConsoleGame.location
{
    public class Location
    {
        public string Name { get; private set; }
        /// <summary>
        /// Coords represent the position of the location in the map
        /// </summary>
        public Coords Coords { get; private set; }
        /// <summary>
        /// Buildings represent an array of the buildings in the location
        /// </summary>
        public Building[] Buildings { get; private set; }
        /// <summary>
        /// Citizens represent an array of the citizens in the location
        /// </summary>
        public Citizen[] Citizens { get; private set; }
        /// <summary>
        /// Type represent the type of the location (town, city, kingdom)
        /// </summary>
        public string Category { get; private set; }
        public Church Church { get; private set; }
        public ArmorShop ArmorShop { get; private set; }
        public WeaponShop WeaponShop { get; private set; }
        public ItemShop ItemShop { get; private set; }
        /// <summary>
        /// Castle represent the castle in the kingdom (if it is)
        /// </summary>
        // TODO
        // public dynamic Castle { get; private set; }

        public Location(string name, Coords coords, Building[] buildings)
        {
            Name = name;
            Coords = coords;
            Buildings = buildings;
            //Church = new Church(new Citizen[2], false, "ttt", new Priest("priest", "priest"));
        }

        [JsonConstructor]
        public Location(
            string name, Coords coords, Building[] buildings, Citizen[] citizens,
            string category, Church church, ArmorShop armorShop, WeaponShop weaponShop, ItemShop itemShop
        )
        {
            Name = name;
            Coords = coords;
            Buildings = buildings;
            Citizens = citizens;
            Category = category;
            Church = church;
            ArmorShop = armorShop;
            WeaponShop = weaponShop;
            ItemShop = itemShop;
        }
    }
}
