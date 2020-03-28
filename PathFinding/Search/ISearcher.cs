using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public interface ISearcher<T>
    {
        // the search method
        SearchInfo<T> Search(ISearchable<T> searchable);
    }
}
