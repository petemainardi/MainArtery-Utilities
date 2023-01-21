using System;
using System.Collections.Generic;

namespace MainArtery.Utilities.Unity
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
    *  Extension methods for the C# List class using the Unity Random class where applicable.
    */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================

    public static class ListExtensions
    {
        // Return a random element and its index from the list --------------------------
        public static Tuple<int, T> Random<T>(this List<T> list)
        {
            int i = UnityEngine.Random.Range(0, list.Count);
            return new Tuple<int, T>(i, list[i]);
        }

        // Return a random element from the list ----------------------------------------
        public static T RandomElement<T>(this List<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        // Return a random element from the list while removing it from the list --------
        public static T RandomRemove<T>(this List<T> list)
        {
            return list.RemoveGrabAt(UnityEngine.Random.Range(0, list.Count));
        }

        // Reorder elements by the Fisher-Yates Shuffle ---------------------------------
        // based on code from https://bost.ocks.org/mike/shuffle/
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
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}