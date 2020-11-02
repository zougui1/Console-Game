using System;
using System.Reflection;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// GetProperties is used to get an array of all the properties of an object
        /// </summary>
        /// <param name="obj">object which you want to get its properties</param>
        /// <returns>return an array of PropertyInfo</returns>
        public static PropertyInfo[] GetProperties(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            return properties;
        }
    }
}
