using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PredefinedVariable<T> : Variable<T>
    {
        public T value;
        public override T Value
        {
            get => value;
        }
    }
}