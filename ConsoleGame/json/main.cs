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
        private static string DataPath { get; } = "data";

        private static string SavePath { get; } = DataPath + "/save.json";
        private static string MonstersPath { get; } = DataPath + "/monsters.json";
        private static string WeaponsPath { get; } = DataPath + "/weapons.json";
        private static string LocationsPath { get; } = DataPath + "/locations.json";
        private static string StatsPath { get; } = DataPath + "/stats.json";
        private static string BuildingsPath { get; } = DataPath + "/buildings.json";
        private static string NPCsPath { get; } = DataPath + "/NPCs.json";
        private static string HeadsPath { get; } = DataPath + "/heads.json";
        private static string TorsosPath { get; } = DataPath + "/torsos.json";
        private static string ArmsPath { get; } = DataPath + "/arms.json";
        private static string LegsPath { get; } = DataPath + "/legs.json";
        private static string FeetsPath { get; } = DataPath + "/feets.json";
        private static string ItemsPath { get; } = DataPath + "/items.json";
        private static string ShieldsPath { get; } = DataPath + "/shields.json";
        private static string ZonesPath { get; } = DataPath + "/zones.json";
        private static string MerchantsPath { get; } = DataPath + "/merchants.json";
        private static string ShopsPath { get; } = DataPath + "/shops.json";
        private static IDictionary<string, string> PathsDictionnary { get; } = new Dictionary<string, string>();

        public static void Init()
        {
            PathsDictionnary.Add("Citizen", NPCsPath);
            PathsDictionnary.Add("Weapon", WeaponsPath);
            PathsDictionnary.Add("Building", BuildingsPath);
            PathsDictionnary.Add("Head", HeadsPath);
            PathsDictionnary.Add("Torso", TorsosPath);
            PathsDictionnary.Add("Arm", ArmsPath);
            PathsDictionnary.Add("Leg", LegsPath);
            PathsDictionnary.Add("Feet", FeetsPath);
            PathsDictionnary.Add("Item", ItemsPath);
            PathsDictionnary.Add("Shield", ShieldsPath);
            PathsDictionnary.Add("ArmorShop", ShopsPath);
            PathsDictionnary.Add("WeaponShop", ShopsPath);
            PathsDictionnary.Add("ItemShop", ShopsPath);
            PathsDictionnary.Add("WeaponMerchant", MerchantsPath);
            PathsDictionnary.Add("ArmorMerchant", MerchantsPath);
            PathsDictionnary.Add("ItemMerchant", MerchantsPath);
        }

        /// <summary>
        /// GetAndParseId is used to get the id of a JToken object and parse it
        /// </summary>
        /// <param name="jToken">The object to get the id from</param>
        /// <returns>return an id of type int</returns>
        private static int GetAndParseId(JToken jToken)
        {
            return ParseInt(jToken["id"]);
        }

        /// <summary>
        /// ParseId is used to parse an id which is of type JToken
        /// </summary>
        /// <param name="id">the id to parse</param>
        /// <returns>return an id of type int</returns>
        private static int ParseInt(JToken id)
        {
            return int.Parse(id.ToString());
        }

        /// <summary>
        /// GetJTokenById is used to get a JToken (json string object) from a json file based on the given id
        /// </summary>
        /// <param name="path">the path where the json is</param>
        /// <param name="id">the id of the object we want to retrieve</param>
        /// <param name="result">the result JToken</param>
        /// <returns>return true if there's on object with the given id, otherwise return false</returns>
        public static bool GetJTokenById(string path, int id, out JToken result)
        {
            JObject jObject = GetFile(path);

            foreach (JToken jToken in jObject["data"])
            {
                if (GetAndParseId(jToken) == id)
                {
                    result = jToken;
                    return true;
                }
            }
            result = null;
            return false;
        }

        /// <summary>
        /// GetJTokenById is used to get a JToken (json string object) from a json file based on the given id
        /// </summary>
        /// <param name="path">the path where the json is</param>
        /// <param name="str">the string to test of the object we want to retrieve</param>
        /// <param name="propertyName">the property where we want to test the given string</param>
        /// <param name="result">the result JToken</param>
        /// <returns>return true if there's on object with the given id, otherwise return false</returns>
        public static bool GetJTokenByString(string path, string str, string propertyName, out JToken result)
        {
            JObject jObject = GetFile(path);

            foreach (JToken jToken in jObject["data"])
            {
                if (jToken[propertyName].ToString() == str)
                {
                    result = jToken;
                    return true;
                }
            }
            result = null;
            return false;
        }

        /// <summary>
        /// ToObject is used to parse a json object into an object, also getting nested object which is the ID of the said object
        /// </summary>
        /// <typeparam name="T">the type of any object related to the json files</typeparam>
        /// <param name="jToken">the json object to parse</param>
        /// <returns>return the parsed object of the given type</returns>
        public static T ToObject<T>(JToken jToken)
        {
            T result;
            result = jToken.ToObject<T>();

            if(jToken["Citizens_id"] != null || jToken["Citizen_id"] != null)
            {
                GetNestedObject<Citizen>(jToken, result);
            }

            if (jToken["Weapon_id"] != null || jToken["Weapons_id"] != null)
            {
                GetNestedObject<Weapon>(jToken, result);
            }

            if (jToken["Head_id"] != null || jToken["Torso_id"] != null || jToken["Arms_id"] != null || jToken["Legs_id"] != null || jToken["Feet_id"] != null)
            {
                GetNestedObject<Armor>(jToken, result);
            }

            if (jToken["Items_id"] != null || jToken["Item_id"] != null)
            {
                GetNestedObject<Item>(jToken, result);
            }

            if (jToken["Buildings_id"] != null || jToken["Building_id"] != null)
            {
                GetNestedObject<Building>(jToken, result);
            }

            if (jToken["Shield_id"] != null)
            {
                GetNestedObject<Shield>(jToken, result);
            }

            if (jToken["WeaponShop_id"] != null)
            {
                GetNestedObject<WeaponShop>(jToken, result);
            }

            if (jToken["ArmorShop_id"] != null)
            {
                GetNestedObject<ArmorShop>(jToken, result);
            }

            if (jToken["ItemShop_id"] != null)
            {
                GetNestedObject<ItemShop>(jToken, result);
            }

            if (jToken["Church_id"] != null)
            {
                GetNestedObject<Church>(jToken, result);
            }

            if (jToken["WeaponMerchant_id"] != null)
            {
                GetNestedObject<WeaponMerchant>(jToken, result);
            }

            if (jToken["ArmorMerchant_id"] != null)
            {
                GetNestedObject<ArmorMerchant>(jToken, result);
            }

            if (jToken["ItemMerchant_id"] != null)
            {
                GetNestedObject<ItemMerchant>(jToken, result);
            }

            return result;
        }

        /// <summary>
        /// GetNestedObject is used to get all the nested objects which are the ID of the said objects and not directly the said objects
        /// </summary>
        /// <typeparam name="T">The type of the wanted object</typeparam>
        /// <param name="jToken">the json object which contain the object</param>
        /// <param name="result">result is the parsed object that contain the nested ones</param>
        public static void GetNestedObject<T>(JToken jToken, dynamic result)
        {
            foreach(JToken nestedJToken in jToken)
            {
                string current = nestedJToken.ToString();
                int indexId = current.IndexOf("_id");
                string lastNamespace = Utils.GetLastNamespace(typeof(T).ToString());

                if(indexId >= 0 && current.IndexOf(lastNamespace) >= 0)
                {
                    string dirtyStr = current.Substring(0, indexId);
                    string cleanStr = dirtyStr.Substring(1, dirtyStr.Length - 1);
                    string singularCleanStr = (cleanStr.LastIndexOf('s') == cleanStr.Length - 1)
                        ? cleanStr.Substring(0, cleanStr.Length - 1)
                        : cleanStr;

                    string filePath = "";

                    if (PathsDictionnary.ContainsKey(singularCleanStr))
                    {
                        filePath = PathsDictionnary[singularCleanStr];
                    }
                    else
                    {
                        throw new Exception($"the paths dictionnary does not contain the key \"{singularCleanStr}\"");
                    }

                    if (cleanStr.LastIndexOf('s') == cleanStr.Length - 1)
                    {
                        int[] idArray = Utils.ParseIntArray(jToken[$"{cleanStr}_id"].ToArray());
                        T[] nestedObjects = new T[idArray.Length];

                        for (int i = 0; i < idArray.Length; ++i)
                        {
                            GetJTokenById(filePath, int.Parse(idArray[i].ToString()), out JToken subJToken);
                            nestedObjects[i] = ToObject<T>(subJToken);
                        }
                        Utils.Setter(result, cleanStr, nestedObjects);
                    }
                    else
                    {
                        int id = int.Parse(jToken[$"{cleanStr}_id"].ToString());
                        T nestedObject;
                        
                        GetJTokenById(filePath, id, out JToken subJToken);
                        nestedObject = ToObject<T>(subJToken);

                        Utils.Setter(result, cleanStr, nestedObject);
                    }
                }
            }
        }

        /// <summary>
        /// GetFile is used to get a json string of type JObject
        /// </summary>
        /// <param name="path">The path of the json file we want to retrieve</param>
        /// <returns>an object which contain the json we retrieved</returns>
        public static JObject GetFile(string path)
        {
            using (StreamReader file = File.OpenText(@path))
            {
                return JObject.Parse(file.ReadToEnd());
            }
        }
    }
}