using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class Spear : BaseWeapon, IUseable
    {
        [SerializeField] private Variable<float> handlingSpeedVariable;
        private void Awake()
        {
            InitStateMachine();
        }

        private void Update()
        {
            StateMachine.Update();
        }

        public void Use()
        {
            
        }

        public override void InitStateMachine()
        {
            StateMachine = new BaseStateMachine();
            StateMachine.InitNewState(new SpearIdleState(gameObject, handlingSpeedVariable.Value));
        }
    }
}