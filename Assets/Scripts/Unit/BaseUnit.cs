using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;

namespace Unit
{
    public class BaseUnit : MonoBehaviour, IDamagable
    {
        public event Action<int> OnTakeDamage;
        public event Action<BaseUnit> OnTargetedBy;

        [SerializeField] private Variable<int> healthVariable;

        private BaseStateMachine stateMachine;
        private IDamagable health;
        private IUseable weapon;

        public BaseUnit target;
        private void Awake()
        {
            InitWeapon();
            InitHealth();
            InitStateMachine();
        }

        private void Update()
        {
            stateMachine.Update();
        }

        private void InitStateMachine()
        {
            stateMachine = new BaseStateMachine();
            stateMachine.InitNewState(new SearchingState(this));
        }

        private void InitHealth() // Wraping Health Decorator.
        {
            var health = new Health(healthVariable.Value);
            this.health = new BaseUnitHealth(health);
            //unitHealth = new Health(healthVariable.Value); // Without Decoration.
            health.OnDie += Die;
        }

        private void InitWeapon()
        {
            weapon = GetComponentInChildren<IUseable>(); // I'd rather use DI container for weapon injection, but don't really want to install whole Zenject just for that.
        }

        public void TakeDamage(int damage) => health.TakeDamage(damage);

        private void Die()
        {
            Debug.Log("Health Depleted");
        }

        public void Target(BaseUnit unit) => OnTargetedBy?.Invoke(unit);
    }
}