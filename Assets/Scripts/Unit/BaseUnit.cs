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

        private BaseStateMachine stateMachine;
        private IDamagable health;
        private IUseable weapon;
        private IMatchmakingService baseUnitMatchmaker;

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

        private void OnDestroy()
        {
            stateMachine.Destroy();
        }

        public void TakeDamage(int damage) => OnTakeDamage?.Invoke(damage);

        private void InitStateMachine()
        {
            stateMachine = new BaseStateMachine();
            stateMachine.InitNewState(new SearchingState(this, baseUnitMatchmaker, SetTarget));
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

        private void SearchForTarget() => stateMachine.ChangeState(new SearchingState(this, baseUnitMatchmaker, SetTarget));

        private void Die() => stateMachine.ChangeState(new DeathState(this));

        private void SetTarget(BaseUnit target) => stateMachine.ChangeState(new ApproachmentState(this, target, SetFight, SearchForTarget));

        private void SetFight(BaseUnit target) => stateMachine.ChangeState(new FightingState(this, target, weapon, health, SetTarget, SearchForTarget));
    }
}