using Core;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Unit
{
    public class FightingState : BaseState
    {
        private const float rotationSpeed = 0.3f;
        private const float minimumTargetDistance = 4.5f;
        private const float minHitDelay = 1f;
        private const float maxHitDelay = 2f;
        private BaseUnit self;
        private BaseUnit target;
        private IUseable weapon;
        private IDamagable damagable;
        private Action<BaseUnit> onTargetOutOfRange;
        private Action onTargetLost;
        private Coroutine fighting;
        public FightingState(BaseUnit self, BaseUnit target, IUseable weapon, IDamagable damagable, Action<BaseUnit> onTargetOutOfRange, Action onTargetLost)
        {
            this.self = self;
            this.target = target;
            this.weapon = weapon;
            this.onTargetOutOfRange = onTargetOutOfRange;
            this.onTargetLost = onTargetLost;
            this.damagable = damagable;
            self.OnTakeDamage += TakeDamage;
        }

        public override void Enter()
        {
            fighting = self.StartCoroutine(Fighting());
        }

        public override void Exit()
        {
            self.StopCoroutine(fighting);
            self.OnTakeDamage -= TakeDamage;
        }

        public override void Update()
        {
            if (!ValidateTarget())
                return;
            LookOnTarget();
            CheckAwayFromTarget();
        }

        public bool ValidateTarget()
        {
            if (target != null)
                return true;
            onTargetLost?.Invoke();
            return false;
        }

        public void LookOnTarget()
        {
            self.transform.rotation = Quaternion.Slerp(self.transform.rotation, Quaternion.LookRotation(target.transform.position - self.transform.position), rotationSpeed);
        }

        public void CheckAwayFromTarget()
        {
            if (Vector3.Distance(self.transform.position, target.transform.position) > minimumTargetDistance)
                onTargetOutOfRange?.Invoke(target);
        }

        public IEnumerator Fighting()
        {
            var hitDelay = Random.Range(minHitDelay, maxHitDelay);
            while (true)
            {
                yield return new WaitForSeconds(hitDelay);
                weapon.Use();
            }
        }

        public void TakeDamage(int damage)
        {
            damagable.TakeDamage(damage);
        }

        public override void Destroy()
        {
            
        }
    }
}