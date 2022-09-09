using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public abstract class Variable<T> : ScriptableObject
    {
        public virtual T Value {
            get;
            private set;
        }
    }
}