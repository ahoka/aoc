using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Day6
    {
        private static Regex pattern = new Regex("(?<x>[0-9]+), (?<y>[0-9]+)");
        public static IEnumerable<Coordinate> Input => File.ReadAllLines("input6.txt")
            .Select(x => pattern.Match(x).Groups)
            .Select(x => new Coordinate(int.Parse(x["x"].Value), int.Parse(x["y"].Value)));

        public static int SolveFirst()
        {
            var input = Input.ToArray();

            var distanceMatrix = new int[input.Length, input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    distanceMatrix[i, j] = input[i].ManhattenDistance(input[j]);
                }
            }

            return 0;
        }
    }
}
