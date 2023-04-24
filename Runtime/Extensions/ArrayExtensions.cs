using System;

namespace MainArtery.Utilities
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     *  Extension methods for the C# Array class (with the System Random class).
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    public static class ArrayExtensions
    {
        // Return a random element and its index from the array -------------------------
        public static Tuple<int, T> Random<T>(this T[] array, Random rand)
        {
            int i = rand.Next(0, array.Length);
            return new Tuple<int, T>(i, array[i]);
        }

        // Return a random element from the array ---------------------------------------
        public static T RandomElement<T>(this T[] array, Random rand)
        {
            return array[rand.Next(0, array.Length)];
        }

        // Reorder elements by the Fisher-Yates Shuffle ---------------------------------
        // based on code from https://bost.ocks.org/mike/shuffle/
        public static void Shuffle<T>(this T[] array, Random rand)
        {
            int idx, count = array.Length;
            T temp;

            while (count > 0)
            {
                idx = rand.Next(0, count--);
                temp = array[count];
                array[count] = array[idx];
                array[idx] = temp;
            }
        }
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}
