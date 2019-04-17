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
using ConsoleGame.game;
using ConsoleGame.items;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.items.stuff.handed.shields;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.location;
using ConsoleGame.misc;
using ConsoleGame.misc.map;
using ConsoleGame.utils;

namespace ConsoleGame.json
{
    public static partial class Json
    {
        /// <summary>
        /// Save is used to save the party
        /// it convert the current Game into a json string and write it in a file
        /// </summary>
        /// <param name="game">The party to save into a json file</param>
        public static void Save(Game game)
        {
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
                throw e;
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
        
        public static InitStats GetInitStats(string className)
        {
            GetJTokenByString(StatsPath, className, "Class", out JToken jToken);
            return jToken.ToObject<InitStats>();
        }
        
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

        public static Item GetRightItem(int id, string dataType)
        {
            JToken jToken;

            switch (dataType)
            {
                case "weapons":
                    GetJTokenById(WeaponsPath, id, out jToken);
                    return ToObject<Weapon>(jToken);
                case "heads":
                    GetJTokenById(HeadsPath, id, out jToken);
                    return ToObject<Armor>(jToken);
                case "torsos":
                    GetJTokenById(TorsosPath, id, out jToken);
                    return ToObject<Armor>(jToken);
                case "arms":
                    GetJTokenById(ArmsPath, id, out jToken);
                    return ToObject<Armor>(jToken);
                case "legs":
                    GetJTokenById(LegsPath, id, out jToken);
                    return ToObject<Armor>(jToken);
                case "feets":
                    GetJTokenById(FeetsPath, id, out jToken);
                    return ToObject<Armor>(jToken);
                case "items":
                    GetJTokenById(ItemsPath, id, out jToken);
                    return ToObject<Item>(jToken);
                default:
                    throw new Exception($"The data-type \"{dataType}\" is not handled");
            }
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

        public static Zone GetZone(int id)
        {
            GetJTokenById(ZonesPath, id, out JToken jToken);
            return ToObject<Zone>(jToken);
        }

        public static Zone GetCurrentZone(User user, Zone currentZone)
        {
            JObject file = GetFile(ZonesPath);

            foreach(JToken zone in file["data"])
            {
                int id = int.Parse(zone["id"].ToString());

                if(id != currentZone.Id)
                {
                    JToken coords = zone["Coords"];
                    JToken rect = zone["Rect"];

                    int x = int.Parse(coords["X"].ToString());
                    int y = int.Parse(coords["Y"].ToString());
                    int width = int.Parse(rect["Width"].ToString());
                    int height = int.Parse(rect["Height"].ToString());
                    
                    // seems to work, should test with more zones
                    if ((user.Coords.X >= x && user.Coords.X <= (x + width)) && (user.Coords.Y >= y && user.Coords.Y <= (y + height)))
                    {
                        return ToObject<Zone>(zone);
                    }
                }
            }
            
            return currentZone;
        }
    }
}