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

            var areas = new float[input.Length];

            int startI = input.Min(c => c.X) - 1;
            int endI = input.Max(c => c.X) + 1;
            int startJ = input.Min(c => c.Y) - 1;
            int endJ = input.Max(c => c.Y) + 1;

            for (int i = startI; i < endI; i++)
            {
                for (int j = startJ; j < endJ; j++)
                {
                    var distanceFromCoordinates = input
                        .Select(x => x.ManhattenDistance(new Coordinate(i, j)))
                        .ToList();

                    var min = distanceFromCoordinates.FindMin();

                    if (i == startI || j == startJ || i == endI - 1 || j == endJ - 1)
                    {
                        areas[min] = float.PositiveInfinity;
                    }
                    else
                    {
                        areas[min] += 1;
                    }
                }
            }

            var result = areas.Where(x => !float.IsPositiveInfinity(x))
                .OrderByDescending(x => x)
                .First();

            return (int)result;
        }

        public static int SolveSecond()
        {
            var input = Input.ToArray();
            int isNear = 0;

            int startI = input.Min(c => c.X) - 1;
            int endI = input.Max(c => c.X) + 1;
            int startJ = input.Min(c => c.Y) - 1;
            int endJ = input.Max(c => c.Y) + 1;

            for (int i = startI; i < endI; i++)
            {
                for (int j = startJ; j < endJ; j++)
                {
                    var distanceSum = input
                        .Select(x => x.ManhattenDistance(new Coordinate(i, j)))
                        .Sum();

                    if (distanceSum < 10_000)
                    {
                        isNear += 1;
                    }
                }
            }

            var result = isNear;

            return result;
        }
    }
}
