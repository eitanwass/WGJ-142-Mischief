using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{

    class CompareHeuristicStates<T> : IComparer<State<T>>
    {
        public HeuristicFunction<T> func;

        public CompareHeuristicStates(HeuristicFunction<T> func)
        {
            this.func = func;
        }

        public int Compare(State<T> x, State<T> y)
        {
            return (x.Cost + func.Calc(x)) - (y.Cost + func.Calc(y));
        }
    }
}
