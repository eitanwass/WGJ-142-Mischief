using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class Point
    {

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Equals(Point p)
        {
            return this.X == p.X && this.Y == p.Y;
        }
    };
}