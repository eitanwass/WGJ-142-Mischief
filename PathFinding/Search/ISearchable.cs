using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public interface ISearchable<T>
    {
        State<T> GetInitialState();
        State<T> GetGoalState();
        List<State<T>> GetAllPossibleStates(State<T> s);
    }
}

