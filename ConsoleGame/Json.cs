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
using ConsoleGame.location.locationTypes;
using ConsoleGame.misc;

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
            return GetObjectFromData(s_itemsPath, id);
            /*JObject file = GetFile(s_itemsPath);
            foreach(JToken item in file["data"])
            {
                if(GetAndParseId(item) == id)
                {
                    return item.ToObject<Item>();
                }
            }
            return null;*/
        }

        public static Shield GetShield(int id)
        {
            return GetObjectFromData(s_shieldsPath, id);
            /*JObject file = GetFile(s_shieldsPath);
            foreach (JToken shield in file["data"])
            {
                if (GetAndParseId(shield) == id)
                {
                    return shield.ToObject<Shield>();
                }
            }
            return null;*/
        }

        /*public static Armor GetArmor(int id)
        {
            JObject file = GetFile(s_armorsPath);
            foreach (JToken armor in file["data"])
            {
                if (GetAndParseId(armor) == id)
                {
                    return armor.ToObject<Armor>();
                }
            }
            return null;
        }*/

        public static Armor GetArmor(int id)
        {
            return GetObjectFromData(s_armorsPath, id, "Armor");
        }

        public static dynamic GetObjectFromData(string path, int id, string overType = null, string dataContainer = "data")
        {
            JObject jObject = GetFile(path);
            foreach(JToken jToken in jObject[dataContainer])
            {
                if(GetAndParseId(jToken) == id)
                {

                    GetNestedObjects(jToken);

                    string toTest = overType == null ? jToken["Type"].ToString() : overType;
                    switch (toTest)
                    {
                        case "Armor":
                            return jToken.ToObject<Armor>();
                        case "ArmorMerchant":
                            return jToken.ToObject<ArmorMerchant>();
                        case "ArmorShop":
                            return jToken.ToObject<ArmorShop>();
                        case "Building":
                            return jToken.ToObject<Building>();
                        case "Church":
                            return jToken.ToObject<Church>();
                        case "Citizen":
                            return jToken.ToObject<Citizen>();
                        case "City":
                            return jToken.ToObject<City>();
                        case "Item":
                            return jToken.ToObject<Item>();
                        case "ItemMerchant":
                            return jToken.ToObject<ItemMerchant>();
                        case "ItemShop":
                            return jToken.ToObject<ItemShop>();
                        case "Kingdom":
                            return jToken.ToObject<Kingdom>();
                        case "Monster":
                            return jToken.ToObject<Monster>();
                        case "Priest":
                            return jToken.ToObject<Priest>();
                        case "Shield":
                            return jToken.ToObject<Shield>();
                        case "Spell":
                            return jToken.ToObject<Spell>();
                        case "Town":
                            return jToken.ToObject<Town>();
                        case "Weapon":
                            return jToken.ToObject<Weapon>();
                        case "WeaponMerchant":
                            return jToken.ToObject<WeaponMerchant>();
                        case "WeaponShop":
                            return jToken.ToObject<WeaponShop>();
                        default: return null;
                    }
                }
            }
            return null;
        }

        public static AbstractNPC GetNPC(int id)
        {
            return GetObjectFromData(s_NPCsPath, id);
            /*JObject file = GetFile(s_NPCsPath);
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
            return null;*/
        }

        public static Building GetBuilding(int id)
        {
            return GetObjectFromData(s_buildingsPath, id);
            /*JObject file = GetFile(s_buildingsPath);
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
            return null;*/
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
                        GetNestedObjects(monster);

                        weapon = GetWeapon(int.Parse(monster["Weapon"].ToString()));
                    }
                }
                return result;
            }
        }

        public static void GetNestedObjects(JToken jToken)
        {
            foreach(JToken nestedJToken in jToken)
            {
                if(jToken["NPC_id"] != null)
                {
                    string current = nestedJToken.ToString();
                    int indexId = current.IndexOf("_id");
                    if (indexId >= 0)
                    {
                        Console.WriteLine("{0}: {1}", current, indexId);
                        string dirtyStr = current.Substring(0, indexId);
                        utils.Utils.Cconsole.Color("Blue").WriteLine(dirtyStr.Substring(1, dirtyStr.Length - 1));
                        jToken.AddAfterSelf(GetWeapon(0));
                        // object { "type": "MyType", "id": 0} in the json
                    }
                }
            }
        }

        public static Weapon GetWeapon(int id)
        {
            return GetObjectFromData(s_weaponsPath, id, "Weapon");
            /*JObject weapons = GetFile(s_weaponsPath);
            
            foreach (JToken weapon in weapons["data"])
            {
                if (GetAndParseId(weapon) == id)
                {
                    return weapon.ToObject<Weapon>();
                }
            }
            return null;*/
        }

        public static Location GetLocation(int id)
        {
            return GetObjectFromData(s_locationsPath, id);
            /*JObject file = GetFile(s_locationsPath);
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
            return null;*/
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