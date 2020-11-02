namespace ConsoleGame.utils
{
    public static partial class Utils
    {
        public static void FillArray(object[] array, object[] elements, bool nested = false)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                if (elements[i] != null)
                {
                    if (nested)
                    {
                        array[i] = elements;
                    }
                    else
                    {
                        array[i] = elements[i];
                    }
                }
            }
        }

        public static void FillArray(object[] array, object element)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                array[i] = element;
            }
        }
    }
}
