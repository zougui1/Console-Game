using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        /// <summary>
        /// InEnum is used to know if a property is in an enum
        /// </summary>
        /// <param name="needle">a string of the property we want to know if its in an enum</param>
        /// <param name="enumType">the enum we want to know if the given property is in</param>
        /// <returns>return true if the property is in the enum, otherwise false</returns>
        public static bool InEnum(string needle, Type enumType)
        {
            foreach (string item in Enum.GetNames(enumType))
            {
                if (item == needle)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
