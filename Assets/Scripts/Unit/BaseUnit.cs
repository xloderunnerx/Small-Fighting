using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class BaseUnit : MonoBehaviour, IDamagable
    {
        [SerializeField] private Variable<int> healthVariable;

        public event Action<int> OnTakeDamage;
        public event Action<BaseUnit> OnTargetedBy;

        private BaseStateMachine stateMachine;
        private IDamagable health;
        private IUseable weapon;
        private IMatchmakingService baseUnitMatchmaker;

        public BaseUnit target; // Debug purposes

        [Inject]
        public void Cinstruct(IMatchmakingService baseUnitMatchmaker)
        {
            this.baseUnitMatchmaker = baseUnitMatchmaker;
        }

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
            stateMachine.InitNewState(new SearchingState(this, baseUnitMatchmaker));
        }

        private void InitHealth() // Wraping Health Decorator.
        {
            var health = new Health(healthVariable.Value);
            this.health = new UnitHealth(health);
            //unitHealth = new Health(healthVariable.Value); // Without Decoration.
            health.OnDie += Die;
        }

        private void InitWeapon()
        {
            weapon = GetComponentInChildren<IUseable>(); // I'd rather use DI container for weapon injection, but don't really want to install whole Zenject just for that.
        }

        private void Die()
        {
            Debug.Log("Health Depleted");
        }

        public void TakeDamage(int damage) => OnTakeDamage?.Invoke(damage);

        public void Target(BaseUnit unit) => OnTargetedBy?.Invoke(unit);
    }
}