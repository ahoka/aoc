using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public static class Algo
    {
        public static int HammingDistance(this string s1, string s2)
        {
            return Enumerable.Zip(s1, s2, (a, b) => a != b ? 1 : 0).Sum();
        }

        public static bool[] ToArray(this int i)
        {
            return Enumerable.Repeat(i, int.MaxValue)
                      .Select((num, shift) => num >> shift)
                      .TakeWhile(x => x != 0)
                      .Select(x => (x & 1) == 1)
                      .ToArray();
        }

        public static BitArray ToBitArray(this int i)
        {
            return new BitArray(i.ToArray());
        }

        public static IEnumerable<IEnumerable<T>> PowerSet<T>(this IEnumerable<T> set)
        {
            return Enumerable.Range(1, (int)Math.Pow(2, set.Count()))
                .Select(p =>
                    Enumerable.Zip(set, p.ToArray(), (item, include) => (item, include))
                        .Where(x => x.include)
                        .Select(x => x.item))
                        .Where(x => x.Any());
        }
    }
}
