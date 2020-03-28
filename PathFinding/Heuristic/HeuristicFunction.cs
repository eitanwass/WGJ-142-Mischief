using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public abstract class HeuristicFunction<T>
    {
        protected State<T> goal;


        public abstract int Calc(State<T> current);

        public void SetGoal(State<T> newGoal) { this.goal = newGoal; }
    }
}
