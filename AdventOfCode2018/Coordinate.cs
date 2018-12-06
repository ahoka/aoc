using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2018
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

        public int ManhattenDistance(Coordinate other) =>
            Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }
}
