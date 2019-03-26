using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ConsoleGame.building;
using ConsoleGame.entity;
using ConsoleGame.entity.stats;
using ConsoleGame.entity.NPC;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.location;
using ConsoleGame.misc;
using ConsoleGame.utils;

namespace ConsoleGame.json
{
    public static partial class Json
    {
        /// <summary>
        /// Save is used to save the party
        /// it convert a Character (the user) into a json string and write it in a file
        /// </summary>
        /// <param name="user">The character to save into a json file</param>
        public static void Save(Game game)
        {
            Console.WriteLine("save");
            string json = JsonConvert.SerializeObject(game, Formatting.Indented);
            File.WriteAllText(SavePath, json);
        }

        /// <summary>
        /// Load is used to load a party from a json file
        /// it deserialize the json and create a Character object from it
        /// </summary>
        /// <returns>return a Character object if the deserialization succeed</returns>
        public static Game Load()
        {
            try
            {
                using (StreamReader file = File.OpenText(SavePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (Game)serializer.Deserialize(file, typeof(Game));
                }
            }
            catch(FileNotFoundException e)
            {
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        public static Item GetItem(int id)
        {
            GetJTokenById(ItemsPath, id, out JToken jToken);
            return ToObject<Item>(jToken);
        }

        public static Shield GetShield(int id)
        {
            GetJTokenById(ShieldsPath, id, out JToken jToken);
            return ToObject<Shield>(jToken);
        }

        public static Armor GetHead(int id)
        {
            GetJTokenById(HeadsPath, id, out JToken jToken);
            return ToObject<Armor>(jToken);
        }

        public static Armor GetTorso(int id)
        {
            GetJTokenById(TorsosPath, id, out JToken jToken);
            return ToObject<Armor>(jToken);
        }

        public static Armor GetArm(int id)
        {
            GetJTokenById(ArmsPath, id, out JToken jToken);
            return ToObject<Armor>(jToken);
        }

        public static Armor GetLeg(int id)
        {
            GetJTokenById(LegsPath, id, out JToken jToken);
            return ToObject<Armor>(jToken);
        }

        public static Armor GetFeet(int id)
        {
            GetJTokenById(FeetsPath, id, out JToken jToken);
            return ToObject<Armor>(jToken);
        }

        public static AbstractNPC GetNPC(int id)
        {
            GetJTokenById(NPCsPath, id, out JToken jToken);
            return ToObject<Citizen>(jToken);
        }

        public static Building GetBuilding(int id)
        {
            GetJTokenById(BuildingsPath, id, out JToken jToken);
            switch (jToken["type"].ToString())
            {
                case "ArmorShop":
                    return ToObject<ArmorShop>(jToken);
                case "Building":
                    return ToObject<Building>(jToken);
                case "Church":
                    return ToObject<Church>(jToken);
                case "ItemShop":
                    return ToObject<ItemShop>(jToken);
                case "WeaponShop":
                    return ToObject<WeaponShop>(jToken);
                default: return null;
            }
        }

        /// <summary>
        /// GetInitStats is used to get the init stats based on the given class's name
        /// </summary>
        /// <param name="className">the class's name we want to retrieve</param>
        /// <returns>return the init stats of type InitStats</returns>
        public static InitStats GetInitStats(string className)
        {
            GetJTokenByString(StatsPath, className, "Class", out JToken jToken);
            return jToken.ToObject<InitStats>();
        }

        /// <summary>
        /// GetInitStats is used to get the stats based on the given class's name
        /// </summary>
        /// <param name="className">the class's name we want to retrieve</param>
        /// <returns>return the stats of type Stats</returns>
        public static Stats GetClassStats(string className)
        {
            GetJTokenByString(StatsPath, className, "Class", out JToken jToken);
            return jToken.ToObject<Stats>();
        }

        public static Monster GetMonster(int id)
        {
            GetJTokenById(MonstersPath, id, out JToken jToken);
            return ToObject<Monster>(jToken);
        }

        public static Weapon GetWeapon(int id)
        {
            GetJTokenById(WeaponsPath, id, out JToken jToken);
            return ToObject<Weapon>(jToken);
        }

        public static Location GetLocation(int id)
        {
            GetJTokenById(LocationsPath, id, out JToken jToken);
            return ToObject<Location>(jToken);
        }

        /// <summary>
        /// GetLocations is used to stock the id of all the locations into a dictionnary with for key their coords
        /// </summary>
        /// <param name="LocationsDict">the dictionnary where we want for key a tuple of the locations's coords and for value their id</param>
        public static void GetLocations(IDictionary<(int X, int Y), int> LocationsDict)
        {
            JObject file = GetFile(LocationsPath);

            foreach (JToken location in file["data"])
            {
                JToken Coords = location["Coords"];

                (int X, int Y) coords;
                coords.X = int.Parse(Coords["X"].ToString());
                coords.Y = int.Parse(Coords["Y"].ToString());
                int id = int.Parse(location["id"].ToString());

                LocationsDict.Add(coords, id);
            }
        }
    }
}