using System;

namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static int[] ParseIntArray(string[] stringArr, bool strict = false)
        {
            int[] tempArr = new int[stringArr.Length];

            for (int i = 0; i < stringArr.Length; ++i)
            {
                tempArr[i] = 0;

                if (!int.TryParse(stringArr[i], out tempArr[i]) && strict)
                {
                    throw new Exception("You tried to parse a non-numeric string");
                }
            }
            return tempArr;
        }

        public static int[] ParseIntArray(object[] stringArr, bool strict = false)
        {
            int[] tempArr = new int[stringArr.Length];

            for (int i = 0; i < stringArr.Length; ++i)
            {
                tempArr[i] = 0;

                if (!int.TryParse(stringArr[i].ToString(), out tempArr[i]) && strict)
                {
                    throw new Exception("You tried to parse a non-numeric string");
                }
            }
            return tempArr;
        }
    }
}
