using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class SearchInfo<T>
    {
        private int cost;
        public List<T> Path { get; }


        public SearchInfo(State<T> endState)
        {

            this.cost = endState == null ? -1 : endState.Cost;

            this.Path = CreatePathFromLastState(endState);
        }

        private List<T> CreatePathFromLastState(State<T> endState)
        {
            List<T> pathList = new List<T>();
            State<T> current = endState;

            while (current != null)
            {
                pathList.Add(current.Value);
                current = current.CameFrom;
            }

            return pathList;
        }
    }
}
