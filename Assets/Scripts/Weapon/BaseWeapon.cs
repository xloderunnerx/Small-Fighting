using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] private Variable<int> damageVariable;

        protected BaseStateMachine StateMachine { get; set; }

        public abstract void InitStateMachine();
    }
}