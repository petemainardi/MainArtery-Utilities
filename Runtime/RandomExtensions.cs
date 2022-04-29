﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MainArtery.Utilities
{
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    /**
     *  Extension methods for the C# Random class.
     */
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
    public static class RandomExtensions
    {
        public static DateTime GetDateRandom(this Random random, DateTime start, DateTime end)
        {
            int days = random.Next(0, (end - start).Days);
            return end.AddDays(-days);
        }


        public static List<T> GetNRandom<T>(this Random random, List<T> items, int numRandom)
        {
            List<T> selectedItems = new List<T>();
            numRandom = Math.Min(numRandom, items.Count);

            for (int i = 0; i < items.Count && numRandom > 0; i++)
            {
                if (random.Next(0, items.Count - i) < numRandom)
                {
                    selectedItems.Add(items[i]);
                    numRandom--;
                }
            }

            return selectedItems;
        }


        /// <summary>
        /// Randomly distributes a set number of items among different categories.
        /// Results are not guaranteed to be exact if the number of supplied items is less than the sum total of category sizes.
        /// </summary>
        /// <typeparam name="T0">
        /// The type of object that is being distributed.
        /// </typeparam>
        /// <typeparam name="T1">
        /// The type of category amongst which objects are being dsitributed.
        /// </typeparam>
        /// <param name="items">
        /// The objects to be distributed.
        /// </param>
        /// <param name="categoryCounts">
        /// Dictionary whose keys are the categories amongst which objects are distributed and whose values are how many objects to distribute to that category.
        /// </param>
        /// <returns>
        /// A dictionary of pairs of items and categories.
        /// </returns>
        public static Dictionary<T0, T1> DistributeNRandom<T0, T1>(this Random random, List<T0> items, Dictionary<T1, int> categoryCounts)
        {
            Dictionary<T0, T1> distributedItems = new Dictionary<T0, T1>();
            Dictionary<T1, int> counts = new Dictionary<T1, int>(categoryCounts);

            int total = counts.Values.Sum();
            List<T0> itemsAdjusted = items.Count > total ? random.GetNRandom(items, total) : items;

            for (int i = 0; i < itemsAdjusted.Count && counts.Count > 0; i++)
            {
                T1 category = counts.Keys.ElementAt(random.Next(0, counts.Count));
                distributedItems.Add(itemsAdjusted[i], category);

                if (--counts[category] <= 0)
                {
                    counts.Remove(category);
                }
            }

            return distributedItems;
        }


        public static Dictionary<T0, T1> DistributeEvenlyRandom<T0, T1>(this Random random, List<T0> items, List<T1> categories)
        {
            int quotient = items.Count / categories.Count;
            int remainder = items.Count % categories.Count;

            Dictionary<T1, int> counts = new Dictionary<T1, int>();
            foreach (T1 category in categories)
                counts.Add(category, quotient);
            foreach (T1 category in random.GetNRandom(categories, remainder))
                counts[category]++;

            return random.DistributeNRandom(items, counts);
        }
    }
    // ============================================================================================
    // ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    // ============================================================================================
}