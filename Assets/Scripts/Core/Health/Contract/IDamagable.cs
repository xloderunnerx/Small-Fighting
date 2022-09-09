using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IDamagable
    {
        void TakeDamage(int damage);
    }
}