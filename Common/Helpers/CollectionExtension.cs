using System;
using System.Collections.Generic;

namespace Common.Helpers
{
    public static class CollectionExtension
    {
        private static readonly Random Rng = new Random();

        public static T RandomElement<T>(this IList<T> list)
        {
            return list[Rng.Next(list.Count)];
        }

        public static T RandomElement<T>(this T[] array)
        {
            return array[Rng.Next(array.Length)];
        }
    }
}