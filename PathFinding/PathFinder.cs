using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class PathFinder
    {
        private Dictionary<string, SearchSolver<Point>> algorithms;

        public PathFinder()
        {
            algorithms["Astar"] = new SearchSolver<Point>(new AStar<Point>(new ManhattanDistance()));
        }

        public List<Point> GetOptimalPath(int[][] table, Point startPoint, Point endPoint)
        {
            return algorithms["Astar"].Solve(new SearchableTable(table, startPoint, endPoint)).Path;
        }

        public List<Point> GetPathByAlgorithm(string alg, int[][] table, Point startPoint, Point endPoint)
        {
            if (!algorithms.ContainsKey(alg))
            {
                throw new KeyNotFoundException("No Such Algorithm Exists");
            }

            return algorithms[alg].Solve(new SearchableTable(table, startPoint, endPoint)).Path;
        }
    }
}
