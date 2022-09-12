using Core;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapon
{
    public class SpearHitState : BaseState, IDisposable
    {
        private Variable<int> damage;
        private Spear spear;
        private Sequence sequence;

        public SpearHitState(Spear spear, Variable<int> damage)
        {
            this.spear = spear;
            this.damage = damage;
        }

        public override void Destroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            sequence.Kill();
        }

        public override void Enter()
        {
            var defaultLocalPosition = spear.transform.localPosition;
            sequence = DOTween.Sequence();
            sequence.Append(spear.transform.DOMove(spear.transform.position + spear.transform.forward * 2, 0.1f));
            sequence.Append(spear.transform.DOLocalMove(defaultLocalPosition, 0.1f));
        }

        public override void Exit()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}