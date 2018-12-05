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

        public static bool IsOppositePolarity(char a, char b)
        {
            if (!char.ToLowerInvariant(a).Equals(char.ToLowerInvariant(b)))
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

        public static string Reduce(string s)
        {
            string prev = s;
            string current = null;
            while (true)
            {
                var sb = new StringBuilder();
                var p = prev.First();   
                foreach (var c in prev.Skip(1))
                {
                    if (p.Equals('*'))
                    {
                        p = c;
                    }
                    else
                    {
                        if (IsOppositePolarity(p, c))
                        {
                            p = '*';
                        }
                        else
                        {
                            sb.Append(p);
                            p = c;
                        }
                    }
                }

                if (current.Equals(prev))
                {
                    return current;
                }

                prev = current;
            }
        }

        // yYacCAx
        public static string SolveFirst()
        {
            var result = Reduce(Input);
            var resultCount = result.Length;

            return result;
        }
    }
}
