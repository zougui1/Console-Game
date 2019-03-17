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
using ConsoleGame.entity.NPC;
using ConsoleGame.items;
using ConsoleGame.items.stuff.handed.weapons;
using ConsoleGame.items.stuff.handed.shields;
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
        private static string s_itemsPath = s_dataPath + "/items.json";
        private static string s_shieldsPath = s_dataPath + "/shields.json";

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

        private static int GetAndParseId(JToken jToken)
        {
            return ParseId(jToken["id"]);
        }

        private static int ParseId(JToken id)
        {
            return int.Parse(id.ToString());
        }

        public static Item GetItem(int id)
        {
            JObject file = GetFile(s_itemsPath);
            foreach(JToken item in file["data"])
            {
                if(GetAndParseId(item) == id)
                {
                    return item.ToObject<Item>();
                }
            }
            return null;
        }

        public static Shield GetShield(int id)
        {
            JObject file = GetFile(s_shieldsPath);
            foreach (JToken shield in file["data"])
            {
                if (GetAndParseId(shield) == id)
                {
                    return shield.ToObject<Shield>();
                }
            }
            return null;
        }

        public static Armor GetArmor(int id)
        {
            JObject file = GetFile(s_armorsPath);
            foreach(JToken armor in file["data"])
            {
                if(GetAndParseId(armor) == id)
                {
                    return armor.ToObject<Armor>();
                }
            }
            return null;
        }

        public static AbstractNPC GetNPC(int id)
        {
            JObject file = GetFile(s_NPCsPath);
            foreach(JToken NPC in file["data"])
            {
                if(GetAndParseId(NPC) == id)
                {
                    switch (NPC["Type"].ToString())
                    {
                        case "Citizen":
                            return NPC.ToObject<Citizen>();
                        case "ItemMerchant":
                            return NPC.ToObject<ItemMerchant>();
                        case "WeaponMerchant":
                            return NPC.ToObject<WeaponMerchant>();
                        case "ArmorMerchant":
                            return NPC.ToObject<ArmorMerchant>();
                        default: return null;
                    }
                }
            }
            return null;
        }

        public static Building GetBuilding(int id)
        {
            JObject file = GetFile(s_buildingsPath);
            foreach(JToken building in file["data"])
            {
                if(GetAndParseId(building) == id)
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
                if (GetAndParseId(weapon) == id)
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
                if(GetAndParseId(location) == id)
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