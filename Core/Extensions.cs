using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlHell_per
{
    internal static class Extensions
    {
        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static T[] Extract<T>(this T[] source, int fromIndex, int length)
        {
            if (null == source)
                throw new ArgumentNullException(nameof(source));
            else if (fromIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(fromIndex),
                                                     "From Index must be non-negative");
            else if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length),
                                                     "Length must be non-negative");

            if (fromIndex >= source.Length || length == 0)
                return new T[0];

            T[] result = new T[Math.Min(length, source.Length - fromIndex)];

            Array.Copy(source, fromIndex, result, 0, result.Length);

            return result;
        }
    }
}
