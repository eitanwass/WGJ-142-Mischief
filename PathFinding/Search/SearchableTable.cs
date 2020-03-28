using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{

    public class SearchableTable : ISearchable<Point>
    {
        private int[][] table;
        private State<Point> start;
        private State<Point> end;

        public SearchableTable(int[][] table, Point startPoint, Point endPoint)
        {
            this.table = table;
            this.start = new State<Point>(startPoint);
            this.end = new State<Point>(startPoint);
        }

        public List<State<Point>> GetAllPossibleStates(State<Point> s)
        {
            bool UP, DOWN, LEFT, RIGHT;

            Point p = s.Value;

            DOWN = p.Y == table.Length - 1 ? false : true;
            RIGHT = p.X == table[0].Length - 1 ? false : true;
            UP = p.Y == 0 ? false : true;
            LEFT = p.X == 0 ? false : true;

            List<State<Point>> possibleStates = new List<State<Point>>();


            // x is horizental (col) and y is vertical (row)
            if (table[p.Y][p.X] == 1)
            {
                return possibleStates;
            }

            if (DOWN && table[p.Y + 1][p.X] != 1)
            {
                State<Point> child = new State<Point>(new Point(p.X, p.Y + 1));
                child.Cost = s.Cost + 1;
                possibleStates.Add(child);
            }


            if (RIGHT && table[p.Y][p.X + 1] != 1)
            {
                State<Point> child = new State<Point>(new Point(p.X + 1, p.Y));
                child.Cost = s.Cost + 1;
                possibleStates.Add(child);
            }

            if (LEFT && table[p.Y][p.X - 1] != 1)
            {
                State<Point> child = new State<Point>(new Point(p.X - 1, p.Y));
                child.Cost = s.Cost + 1;
                possibleStates.Add(child);
            }

            if (UP && table[p.Y - 1][p.X] != 1)
            {
                State<Point> child = new State<Point>(new Point(p.X - 1, p.Y));
                child.Cost = s.Cost + 1;
                possibleStates.Add(child);
            }

            return possibleStates;
        }

        public State<Point> GetGoalState()
        {
            return this.end;
        }

        public State<Point> GetInitialState()
        {
            return this.start;
        }
    }
}

