using System.Collections.Generic;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
    *  Extension methods for the C# List class using the UnityEngine.Random class.
    */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================

    public static class ListExtensions
    {
        /// <summary>
        /// Remove a random element from the list and return it.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the list</typeparam>
        /// <param name="list">The list being acted upon</param>
        /// <returns>The randomly selected item that was removed</returns>
        public static T RandomRemove<T>(this List<T> list)
        {
            return list.RemoveGrabAt(UnityEngine.Random.Range(0, list.Count));
        }

        /// <summary>
        /// Reorder elements by the Fisher-Yates Shuffle.
        /// </summary>
        /// <remarks>Based on code from https://bost.ocks.org/mike/shuffle/</remarks>
        /// <typeparam name="T">The type of objects contained in the list</typeparam>
        /// <param name="list">The list being acted upon</param>
        public static void Shuffle<T>(this List<T> list)
        {
            int idx, count = list.Count;
            T temp;

            while (count > 0)
            {
                idx = UnityEngine.Random.Range(0, count--);
                temp = list[count];
                list[count] = list[idx];
                list[idx] = temp;
            }
        }
    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}