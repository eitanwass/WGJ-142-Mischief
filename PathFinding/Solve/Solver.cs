using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public interface Solver<Problem, Solution>
    {
        Solution Solve(Problem problem);
    }
}
