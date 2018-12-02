using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2018
{
    public class Day2
    {
        public static int SolveFirst(string input)
        {
            int twos = 0;
            int threes = 0;

            foreach (var code in File.ReadAllLines(input))
            {
                var letters = new Dictionary<char, int>();
                foreach (var letter in code)
                {
                    if (letters.TryGetValue(letter, out var value))
                    {
                        letters[letter] = value + 1;
                    }
                    else
                    {
                        letters[letter] = 1;
                    }
                }

                if (letters.Where(x => x.Value == 3).Any())
                {
                    threes++;
                }

                if (letters.Where(x => x.Value == 2).Any())
                {
                    twos++;
                }
            }

            return twos * threes;
        }

        private static IEnumerable<string> Input => File.ReadAllLines("input2.txt");

        public static string SolveSecond()
        {
            foreach (var code1 in Input)
            {
                foreach (var code2 in Input)
                {
                    if (code1.Length != code2.Length)
                    {
                        throw new ArgumentException($"Length mismatch: '{code1}' '{code2}'");
                    }

                    if (Algo.HammingDistance(code1, code2) == 1)
                    {
                        var result = Enumerable.Zip(code1, code2, (a, b) => (a, b))
                            .Where(x => x.a == x.b)
                            .Select(x => x.a);

                        return new string(result.ToArray());
                    }
                }
            }

            return null;
        }
    }
}
