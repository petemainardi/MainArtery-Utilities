﻿using System;
using System.Collections.Generic;

namespace MainArtery.Utilities
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     *  Extension methods for the C# List class (with the System Random class).
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    public static class ListExtenstions
    {
        // Remove the given element from the list and return it -------------------------
        public static T RemoveGrab<T>(this List<T> list, T element)
        {
            if (list.Remove(element))
                return element;

            return default;
        }

        // Remove the element at the given index and return it --------------------------
        public static T RemoveGrabAt<T>(this List<T> list, int index)
        {
            if (index < 0 || index >= list.Count)
                return default;

            T t = list[index];
            list.RemoveAt(index);
            return t;
        }

        // Return a random element and its index from the list --------------------------
        public static Tuple<int, T> Random<T>(this List<T> list, Random rand)
        {
            int i = rand.Next(0, list.Count);
            return new Tuple<int, T>(i, list[i]);
        }

        // Return a random element from the list ----------------------------------------
        public static T RandomElement<T>(this List<T> list, Random rand)
        {
            return list[rand.Next(0, list.Count)];
        }

        // Return a random element from the list while removing it from the list --------
        public static T RandomRemove<T>(this List<T> list, Random rand)
        {
            return list.RemoveGrabAt(rand.Next(0, list.Count));
        }

        // Reorder elements by the Fisher-Yates Shuffle  --------------------------------
        // based on code from https://bost.ocks.org/mike/shuffle/
        public static void Shuffle<T>(this List<T> list, Random rand)
        {
            int idx, count = list.Count;
            T temp;

            while (count > 0)
            {
                idx = (int)Math.Floor(rand.NextDouble() * count--);
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