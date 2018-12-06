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
        public class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; }
            public int Y { get; }
        }

        private static Regex pattern = new Regex("(?<x>). (?<y>)");
        public IEnumerable<Coordinate> Input => File.ReadAllLines("input6.txt")
            .Select(x => pattern.Match(x).Groups)
            .Select(x => new Coordinate(int.Parse(x['x'].Value), int.Parse(x['y'].Value)));

        public static int SolveFirst()
        {

        }
    }
}
