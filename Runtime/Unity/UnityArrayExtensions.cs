
namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
    *   Extension methods for the C# Array class using the UnityEngine.Random class.
    */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================

    public static class ArrayExtenstions
    {
        /// <summary>
        /// Reorder elements by the Fisher-Yates Shuffle.
        /// </summary>
        /// <remarks>Based on code from https://bost.ocks.org/mike/shuffle/</remarks>
        /// <typeparam name="T">The type of objects contained in the array</typeparam>
        /// <param name="array">The array being acted upon</param>
        public static void Shuffle<T>(this T[] array)
        {
            int idx, count = array.Length;
            T temp;

            while (count > 0)
            {
                idx = UnityEngine.Random.Range(0, count--);
                temp = array[count];
                array[count] = array[idx];
                array[idx] = temp;
            }
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}