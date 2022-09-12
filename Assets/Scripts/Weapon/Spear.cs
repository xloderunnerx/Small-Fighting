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

        private void OnDestroy()
        {
            StateMachine.Destroy();
        }

        public void Use()
        {
            StateMachine.ChangeState(new SpearHitState(this, damageVariable));
        }

        public override void InitStateMachine()
        {
            StateMachine = new BaseStateMachine();
            StateMachine.InitNewState(new SpearIdleState(gameObject, handlingSpeedVariable.Value));
        }

        private void OnTriggerEnter(Collider other)
        {
            var damaganble = other.GetComponent<IDamagable>();
            if (damaganble == null)
                return;
            damaganble.TakeDamage(damageVariable.Value);
        }
    }
}