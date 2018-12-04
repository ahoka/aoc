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

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> set)
        {
            return set.SelectMany(x => Permutations(set.Where(y => !y.Equals(x))), (item, perm) => perm.Prepend(item));
        }

        public static int[] Plus(this int[] a, int[] b)
        {
            if (a.Length != b.Length)
            {
                throw new ArgumentException("Array size must be the same");
            }

            return Enumerable.Zip(a, b, (x, y) => x + y).ToArray();
        }

        private static Lazy<int[]> _primes = new Lazy<int[]>(() => CalculatePrimes(1_000_000).ToArray());
        public static IEnumerable<int> Primes => _primes.Value;
        public static bool IsPrime(int number) => Array.BinarySearch(_primes.Value, number) > 0;

        public static IEnumerable<int> CalculatePrimes(int upto)
        {
            var sieve = new bool[upto];

            var limit = (int)Math.Sqrt(upto);
            for (int i = 2; i < limit; i++)
            {
                if (!sieve[i])
                {
                    for (int j = (int)Math.Pow(i, 2); j < upto; j += i)
                    {
                        sieve[j] = true;
                    }
                }
            }

            for (int i = 2; i < upto; i++)
            {
                if (!sieve[i])
                {
                    yield return i;
                }
            }
        }
    }
}
