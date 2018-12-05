using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public static class Day5
    {
        public static string Input => File.ReadAllText("input5.txt");

        public static bool ShouldRemove(char a, char b)
        {
            if (IsNotSameUnit(a, b))
            {
                return false;
            }

            if (char.IsLower(a) && char.IsUpper(b) ||
                char.IsUpper(a) && char.IsLower(b))
            {
                return true;
            }

            return false;
        }

        public static bool IsNotSameUnit(char a, char b)
        {
            return !char.ToLowerInvariant(a).Equals(char.ToLowerInvariant(b));
        }

        public static string Reduce(IEnumerable<char> s)
        {
            var a = s.ToList();

            for (int i = 0; i < a.Count - 1;)
            {
                if (ShouldRemove(a[i], a[i + 1]))
                {
                    a.RemoveRange(i, 2);
                    i = Math.Max(i - 1, 0);
                }
                else
                {
                    i++;
                }
            }

            return new string(a.ToArray());
        }

        // yYacCAx
        public static int SolveFirst()
        {
            var result = Reduce(Input);
            var resultCount = result.Length;

            return resultCount;
        }

        public static int SolveSecond()
        {
            var units = Input.ToLowerInvariant()
                             .GroupBy(x => x)
                             .Select(x => x.Key);

            var min = units.Select(unit => Input.Where(x => IsNotSameUnit(x, unit)))
                           .Select(x => Reduce(x).Count())
                           .Min();

            return min;
        }
    }
}
