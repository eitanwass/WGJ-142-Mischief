using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public abstract class HeuristicSearcher<T> : ISearcher<T>
    {
        protected HeuristicFunction<T> func;

        public HeuristicSearcher(HeuristicFunction<T> func)
        {
            this.func = func;
        }

        public abstract SearchInfo<T> Search(ISearchable<T> searchable);
    }
}