using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Health : IDamagable
    {
        public int Value { get; private set; }

        public event Action OnDie;

        public Health(int value)
        {
            Value = value;
        }

        public void TakeDamage(int damage)
        {
            Value -= damage;
            Value = (int)Mathf.Clamp(Value, 0, Mathf.Infinity);
            if (Value == 0)
                OnDie?.Invoke();
        }
    }
}