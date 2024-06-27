using System;
using System.Collections.Generic;
using System.Linq;

namespace MainArtery.Utilities.Unity
{
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    /**
     *  Extension methods for the C# IEnumerable interface using the UnityEngine.Random class.
     */
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Get a random element and its index from the collection.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the collection</typeparam>
        /// <param name="collection">The collection being acted upon</param>
        /// <returns>The index and element randomly selected from the collection</returns>
        public static Tuple<int, T> Random<T>(this IEnumerable<T> collection)
        {
            int i = UnityEngine.Random.Range(0, collection.Count());
            return new Tuple<int, T>(i, collection.ElementAt(i));
        }

        /// <summary>
        /// Get a random element from the collection.
        /// </summary>
        /// <typeparam name="T">The type of objects contained in the collection</typeparam>
        /// <param name="collection">The collection being acted upon</param>
        /// <returns>The element randomly selected from the collection</returns>
        public static T RandomElement<T>(this IEnumerable<T> collection)
        {
            return collection.ElementAt(UnityEngine.Random.Range(0, collection.Count()));
        }

        /// <summary>
        /// Reorder elements by the Fisher-Yates Shuffle.
        /// </summary>
        /// <remarks>Based on code from https://bost.ocks.org/mike/shuffle/</remarks>
        /// <typeparam name="T">The type of objects contained in the collection</typeparam>
        /// <param name="collection">The collection being acted upon</param>
        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> collection)
        {
            T[] result = collection.ToArray();
            int idx, count = collection.Count();
            T temp;

            while (count > 0)
            {
                idx = UnityEngine.Random.Range(0, count--);
                temp = result[count];
                result[count] = result[idx];
                result[idx] = temp;
            }
            return result;
        }

    }
    /// ===========================================================================================
    /// |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// ===========================================================================================
}