using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Claim
    {
        public Claim(int id, int x, int y, int width, int height)
        {
            Id = id;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int Id { get; }
        public int X { get; }
        public int Y { get; }
        public int Width { get; }
        public int Height { get; }
    }


    public class Day3
    {
        // #11 @ 698,565: 15x22
        public static IEnumerable<Claim> Input => File.ReadAllLines("input3.txt")
            .Select(x => Regex.Match(x, "#(?<id>[0-9]+) @ (?<x>[0-9]+),(?<y>[0-9]+): (?<w>[0-9]+)x(?<h>[0-9]+)"))
            .Select(x => new Claim(int.Parse(x.Groups["id"].Value), int.Parse(x.Groups["x"].Value), int.Parse(x.Groups["y"].Value), int.Parse(x.Groups["w"].Value), int.Parse(x.Groups["h"].Value)));

        public static int SolveFirst()
        {
            //var w = Input.Max(x => x.X + x.Width);
            //var h = Input.Max(x => x.Y + x.Height);

            var squares = new int[1000, 1000];

            foreach (var bp in Input)
            {
                for (int x = bp.X; x < bp.X + bp.Width; x++)
                {
                    for (int y = bp.Y; y < bp.Y + bp.Height; y++)
                    {
                        squares[x, y]++;
                    }
                }
            }

            int result = 0;
            for (int x = 0; x < 1000; x++)
            {
                for (int y = 0; y < 1000; y++)
                {
                    if (squares[x, y] > 1)
                    {
                        result++;
                    }
                }
            }

            return result;
        }

        public static int SolveSecond()
        {
            //var w = Input.Max(x => x.X + x.Width);
            //var h = Input.Max(x => x.Y + x.Height);

            var squares = new int[1000, 1000];

            var ids = Input.Select(x => x.Id).ToHashSet();

            foreach (var bp in Input)
            {
                for (int x = bp.X; x < bp.X + bp.Width; x++)
                {
                    for (int y = bp.Y; y < bp.Y + bp.Height; y++)
                    {
                        var current = squares[x, y];
                        if (current == 0)
                        {
                            squares[x, y] = bp.Id;
                        }
                        else
                        {
                            ids.Remove(current);
                            ids.Remove(bp.Id);
                        }
                    }
                }
            }

            return ids.Single();
        }
    }
}
