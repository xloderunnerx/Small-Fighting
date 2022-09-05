using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class SearchingState : BaseState
    {
        private BaseUnit selfUnit;
        private BaseUnit targetUnit;

        public SearchingState(BaseUnit unit)
        {
            this.selfUnit = unit;
            this.selfUnit.OnTargetedBy += TargetBy;
        }

        public override void Enter()
        {
            targetUnit = BaseUnitMatchmaker.GetClosestUnit(selfUnit);
            if(targetUnit == null)
            {
                BaseUnitMatchmaker.OnSearchingUnitAdded += SetTarget;
                return;
            }
            BaseUnitMatchmaker.SetUnitBusy(selfUnit);
            BaseUnitMatchmaker.SetUnitBusy(targetUnit);
            selfUnit.target = targetUnit;
            targetUnit.target = selfUnit;
            var rndColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            selfUnit.GetComponent<MeshRenderer>().material.color = rndColor;
            targetUnit.GetComponent<MeshRenderer>().material.color = rndColor;
            targetUnit.Target(selfUnit);
            StateMachine.ChangeState(new IdleState());
        }

        public override void Exit()
        {
            BaseUnitMatchmaker.OnSearchingUnitAdded -= SetTarget;
        }

        public override void Update()
        {
            
        }

        public void SetTarget(BaseUnit targetUnit)
        {
            if (BaseUnitMatchmaker.IsUnitBusy(targetUnit))
                return;
            if (selfUnit == targetUnit)
                return;
            BaseUnitMatchmaker.SetUnitBusy(selfUnit);
            BaseUnitMatchmaker.SetUnitBusy(targetUnit);
            selfUnit.target = targetUnit;
            targetUnit.target = selfUnit;
            this.targetUnit = targetUnit;
            var rndColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
            selfUnit.GetComponent<MeshRenderer>().material.color = rndColor;
            this.targetUnit.GetComponent<MeshRenderer>().material.color = rndColor;
            targetUnit.Target(selfUnit);
            StateMachine.ChangeState(new IdleState());
        }

        public void TargetBy(BaseUnit baseUnit)
        {
            StateMachine.ChangeState(new IdleState());
        }
    }
}