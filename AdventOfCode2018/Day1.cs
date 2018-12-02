using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    class Day1
    {
        public static int SolveFirst()
        {
            return File.ReadLines("input1.txt")
                .Select(x => int.Parse(x))
                .Sum();
        }

        static IEnumerable<int> Input()
        {
            while (true)
            {
                foreach (var freq in File.ReadLines("input1.txt").Select(x => int.Parse(x)))
                {
                    yield return freq;
                }
            }
        }

        public static int SolveSecond()
        {
            var seen = new HashSet<int>();

            int f = 0;
            foreach (var offset in Input())
            {
                f += offset;
                if (!seen.Add(f))
                {
                    return f;
                }
            }

            return 0;
        }
    }
}
