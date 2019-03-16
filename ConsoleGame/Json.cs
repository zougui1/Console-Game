using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ConsoleGame.entity;
using ConsoleGame.entity.classes;
using ConsoleGame.entity.stats;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.items.stuff.armor;
using ConsoleGame.location;
using ConsoleGame.location.locationTypes;
using ConsoleGame.building;

namespace ConsoleGame
{
    public class Json
    {
        private static string s_dataPath = "data";

        private static string s_savePath = s_dataPath + "/save.json";
        private static string s_monstersPath = s_dataPath + "/monsters.json";
        private static string s_weaponsPath = s_dataPath + "/weapons.json";
        private static string s_locationsPath = s_dataPath + "/locations.json";
        private static string s_statsPath = s_dataPath + "/stats.json";
        private static string s_buildingsPath = s_dataPath + "/buildings.json";
        private static string s_NPCsPath = s_dataPath + "/NPCs.json";
        private static string s_armorsPath = s_dataPath + "/armors.json";

        private static string s_locationNamespace = "ConsoleGame.location";
        private static string s_locationTypesNamespace = "ConsoleGame.location.locationTypes";
        private static string s_itemsNamespace = "ConsoleGame.items";
        private static string s_handedNamespace = s_itemsNamespace + ".stuff.handed";
        private static string s_weaponsNamespace = s_itemsNamespace + ".stuff.handed.weapons";
        private static string s_buildingsNamespace = "ConsoleGame";

        public static void DataDir()
        {
            DirectoryInfo dir = Directory.CreateDirectory(s_dataPath);
        }

        public static void Save(Character user)
        {
            Console.WriteLine("save");
            string json = JsonConvert.SerializeObject(user, Formatting.Indented);
            File.WriteAllText(s_savePath, json);
        }

        public static Character Load()
        {
            try
            {
                using (StreamReader file = File.OpenText(s_savePath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    Character user = (Character)serializer.Deserialize(file, typeof(Character));
                    return user;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Armor GetArmor(int id)
        {
            JObject file = GetFile(s_armorsPath);
            foreach(JToken armor in file["data"])
            {
                if(int.Parse(armor["id"].ToString()) == id)
                {
                    switch (armor["Category"].ToString())
                    {
                        case "Head":
                            return armor.ToObject<Head>();
                        case "Torso":
                            return armor.ToObject<Torso>();
                        case "Arm":
                            return armor.ToObject<Arm>();
                        case "Leg":
                            return armor.ToObject<Leg>();
                        case "Feet":
                            return armor.ToObject<Feet>();
                        default: return null;
                    }
                }
            }
            return null;
        }

        public static NPC GetNPC(int id)
        {
            JObject file = GetFile(s_NPCsPath);
            foreach(JToken NPC in file["data"])
            {
                if(int.Parse(NPC["id"].ToString()) == id)
                {
                    return NPC.ToObject<NPC>();
                }
            }
            return null;
        }

        public static Building GetBuilding(int id)
        {
            JObject file = GetFile(s_buildingsPath);
            foreach(JToken building in file["data"])
            {
                if(int.Parse(building["id"].ToString()) == id)
                {
                    //TODO condition over the building type to retrieve it with the right class
                    switch (building["Type"].ToString())
                    {
                        case "WeaponShop":
                            return building.ToObject<WeaponShop>();
                        default: break;
                    }
                }
            }
            return null;
        }
        
        public static InitStats GetInitStats(string className)
        {
            JObject file = GetFile(s_statsPath);
            foreach(JToken stats in file["data"])
            {
                if(stats["Class"].ToString() == className)
                {
                    return stats.ToObject<InitStats>();
                }
            }

            return null;
        }

        public static Stats GetClassStats(string className)
        {
            JObject file = GetFile(s_statsPath);
            foreach (JToken stats in file["data"])
            {
                if (stats["Class"].ToString() == className)
                {
                    return stats.ToObject<Stats>();
                }
            }

            return null;
        }

        public static EntityStats GetMonster(string name, out Weapon weapon)
        {
            EntityStats result = null;
            weapon = null;
            using (StreamReader file = File.OpenText(@s_monstersPath))
            {
                JObject jObject = JObject.Parse(file.ReadToEnd());
                foreach(JToken monster in jObject["monsters"])
                {
                    if(monster["Name"].ToString() == name)
                    {
                        result = monster.ToObject<EntityStats>();

                        weapon = GetWeapon(int.Parse(monster["Weapon"].ToString()));
                    }
                }
                return result;
            }
        }

        public static Weapon GetWeapon(int id)
        {
            JObject weapons = GetFile(s_weaponsPath);
            
            foreach (JToken weapon in weapons["data"])
            {
                if (int.Parse(weapon["id"].ToString()) == id)
                {
                    return weapon.ToObject<Weapon>();
                }
            }
            return null;
            //return ToObjectById(id, weapons, s_weaponsNamespace, new List<string>() { "Name", "Description", "Damages" });
        }

        public static Location GetLocation(int id)
        {
            JObject file = GetFile(s_locationsPath);
            foreach(JToken location in file["data"])
            {
                if(int.Parse(location["id"].ToString()) == id)
                {
                    switch (location["Type"].ToString())
                    {
                        case "Town":
                            return location.ToObject<Town>();
                        case "City":
                            return location.ToObject<City>();
                        case "Kingdom":
                            return location.ToObject<Kingdom>();
                        default: break;
                    }
                }
            }
            return null;
        }

        public static void GetLocations(IDictionary<(int X, int Y), int> LocationsDict)
        {
            JObject file = GetFile(s_locationsPath);

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

        public static JObject GetFile(string path)
        {
            using (StreamReader file = File.OpenText(@path))
            {
                return JObject.Parse(file.ReadToEnd());
            }
        }
    }
}