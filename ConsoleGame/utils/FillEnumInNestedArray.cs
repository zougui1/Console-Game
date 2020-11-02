using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static object[][] FillEnumInNestedArray(Type enumName)
        {
            Array enumValues = Enum.GetValues(enumName);
            object[][] nestedArray = new object[enumValues.Length][];
            int i = 0;

            foreach (byte value in enumValues)
            {
                nestedArray[i] = new object[] { value };
                ++i;
            }

            return nestedArray;
        }
    }
}
