using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class SearchSolver<T> : Solver<ISearchable<T>, SearchInfo<T>>
    {
        private ISearcher<T> searchAlgoritm;

        public SearchSolver(ISearcher<T> searchAlgoritm)
        {
            this.searchAlgoritm = searchAlgoritm;
        }

        public SearchInfo<T> Solve(ISearchable<T> problem)
        {
            return searchAlgoritm.Search(problem);
        }
    }
}
