using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class ManhattanDistance : HeuristicFunction<Point>
    {
        public override int Calc(State<Point> current)
        {
            return Math.Abs(current.Value.X - goal.Value.X) + Math.Abs(current.Value.Y - goal.Value.Y);
        }
    }
}