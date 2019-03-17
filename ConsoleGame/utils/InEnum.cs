using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static bool InEnum(string needle, Type enumType)
        {
            foreach(string item in Enum.GetNames(enumType))
            {
                if(item == needle)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
