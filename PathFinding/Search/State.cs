using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class State<T>
    {
        public T Value { get; }                    // the state represented by a T
        public int Cost { get; set; }              // cost to reach this state (set by a setter)
        public State<T> CameFrom { get; set; }     // the state we came from to this state (setter)


        public State(T state)
        {
            this.Value = state;
        }

        public bool Equals(State<T> s)
        {
            return Value.Equals(s.Value);
        }
    }
}

