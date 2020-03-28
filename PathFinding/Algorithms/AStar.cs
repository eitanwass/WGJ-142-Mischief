using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class AStar<T> : HeuristicSearcher<T>
    {

        public AStar(HeuristicFunction<T> func) : base(func) { }


        public override SearchInfo<T> Search(ISearchable<T> searchable)
        {
            func.SetGoal(searchable.GetGoalState());

            PriorityQueue<State<T>> open = new PriorityQueue<State<T>>(new CompareHeuristicStates<T>(func));
            HashSet<State<T>> close = new HashSet<State<T>>();
            State<T> current;

            open.Enqueue(searchable.GetInitialState());

            while (!open.Empty())
            {
                current = open.Dequeue();

                close.Add(current);

                if (current.Equals(searchable.GetGoalState()))
                {
                    SearchInfo<T> si = new SearchInfo<T>(current);
                    open.Clear();
                    close.Clear();
                    return si;
                }

                foreach(State<T> s in searchable.GetAllPossibleStates(current))
                {
                    if(!close.Contains(s))
                    {
                        s.CameFrom = current;
                        open.Enqueue(s);
                    }
                }
            }

            open.Clear();
            close.Clear();
            return new SearchInfo<T>(null);
        }
    }
}
